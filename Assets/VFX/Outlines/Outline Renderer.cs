using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineRenderFeature : ScriptableRendererFeature
{
    public Material material = null;
    public OutlineSettings settings;
    private OutlineRenderPass pass;
    public RenderPassEvent _event = RenderPassEvent.AfterRenderingPostProcessing;
    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        RTHandle color = renderer.cameraColorTargetHandle;
        pass.Setup(color);
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }
    public override void Create()
    {
        pass = new OutlineRenderPass(material, settings, name);
        pass.renderPassEvent = _event;
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        pass.ReleaseTargets();
    }
    private void OnDestroy()
    {
        pass.ReleaseTargets();
    }
    private void OnDisable()
    {
        pass.ReleaseTargets();
    }
    [System.Serializable]
     public class OutlineSettings
    {
        public int outlineMaterialPass;
        public Material OutlineMaterial;
        public float KernelSize;
        public List<string> ShaderTags;
        public LayerMask layerMask;
    }

}
class OutlineRenderPass : ScriptableRenderPass
{
    RTHandle rtTemp, rtColor, rtOutlinePass;
    Material blitMaterial;
    Material outlineMaterial;
    int outlineMaterialPass;
    private List<ShaderTagId> shaderTagsList = new List<ShaderTagId>();
    private ProfilingSampler _profilingSampler;
    private FilteringSettings filteringSettings = FilteringSettings.defaultValue;
    public OutlineRenderPass(Material material, OutlineRenderFeature.OutlineSettings settings, string name)
    {
        filteringSettings = new FilteringSettings(RenderQueueRange.opaque, settings.layerMask);;
        this.profilingSampler = new ProfilingSampler(name);
        blitMaterial = material;
        if(settings.ShaderTags.Count > 0)
        {
            foreach (string tag in settings.ShaderTags)
            {
                shaderTagsList.Add(new ShaderTagId(tag));
            }
        }
        shaderTagsList.Add(new ShaderTagId("SRPDefaultUnlit"));
        shaderTagsList.Add(new ShaderTagId("UniversalForward"));
        shaderTagsList.Add(new ShaderTagId("UniversalForwardOnly"));
        blitMaterial.SetFloat("_KernelSize", settings.KernelSize);
        outlineMaterial = settings.OutlineMaterial;
        outlineMaterialPass = settings.outlineMaterialPass;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor colorPassDesc = renderingData.cameraData.cameraTargetDescriptor;
        RenderTextureDescriptor rtOutlinePassDesc = renderingData.cameraData.cameraTargetDescriptor;
        rtOutlinePassDesc.depthBufferBits = 0;
        rtOutlinePassDesc.colorFormat = RenderTextureFormat.Default;
        colorPassDesc.depthBufferBits = 0;
        RenderingUtils.ReAllocateIfNeeded(ref rtTemp, colorPassDesc, name: "_TemporaryColorTexture");
        RenderingUtils.ReAllocateIfNeeded(ref rtOutlinePass, rtOutlinePassDesc, name: "_OutlinePass");
        ConfigureTarget(rtOutlinePass, renderingData.cameraData.renderer.cameraDepthTargetHandle);
        ConfigureClear(ClearFlag.Color, Color.clear);
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, _profilingSampler))
        {

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            if (rtTemp.rt == null || rtColor.rt == null || rtOutlinePass.rt == null || blitMaterial == null)
            {
                return;
            }
            SortingCriteria sortingCriteria = renderingData.cameraData.defaultOpaqueSortFlags;
            DrawingSettings drawingSettings = CreateDrawingSettings(shaderTagsList, ref renderingData, sortingCriteria);
            if(outlineMaterial != null)
            {
                drawingSettings.overrideMaterialPassIndex = outlineMaterialPass;
                drawingSettings.overrideMaterial = outlineMaterial;
            }
            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
            blitMaterial.SetTexture("_OutlinesPass", rtOutlinePass);
            Blitter.BlitCameraTexture(cmd, rtColor, rtTemp, blitMaterial, 0);
            Blitter.BlitCameraTexture(cmd, rtTemp, rtColor, Vector2.one);
        }
        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();
        CommandBufferPool.Release(cmd);
    }
    internal void Setup(RTHandle destColor)
    {
        rtColor = destColor;
    }
    public void ReleaseTargets()
    {
        rtTemp?.Release();
        rtColor?.Release();
        rtOutlinePass?.Release();
    }
}