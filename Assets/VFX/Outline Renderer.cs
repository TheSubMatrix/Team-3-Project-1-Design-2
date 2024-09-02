using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineRenderFeature : ScriptableRendererFeature
{
    public Shader shader;
    private Material material = null;
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
        if (material == null || material.shader != shader && shader != null)
        {
            // only create material if null or different shader has been assigned

            if (material != null) CoreUtils.Destroy(material);
            // destroy material using previous shader

            material = CoreUtils.CreateEngineMaterial(shader);
        }
        if(shader == null)
        {
            if (material != null) CoreUtils.Destroy(material);
        }
        pass = new OutlineRenderPass(material, settings, name);
        pass.renderPassEvent = _event;
    }
    protected override void Dispose(bool disposing)
    {
        pass.ReleaseTargets();
    }
    [System.Serializable]
     public class OutlineSettings
    {
        public List<string> ShaderTags;
        public LayerMask layerMask;
    }

}
class OutlineRenderPass : ScriptableRenderPass
{
    RTHandle rtTemp, rtColor, rtOutlinePass;
    OutlineRenderFeature.OutlineSettings settings;
    Material blitMaterial;
    private List<ShaderTagId> shaderTagsList = new List<ShaderTagId>();
    private ProfilingSampler _profilingSampler;
    private FilteringSettings filteringSettings = FilteringSettings.defaultValue;
    public OutlineRenderPass(Material material, OutlineRenderFeature.OutlineSettings settings, string name)
    {
        filteringSettings = new FilteringSettings(RenderQueueRange.opaque, settings.layerMask);
        this.settings = settings;
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
    }
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
        desc.depthBufferBits = 0;
        RenderingUtils.ReAllocateIfNeeded(ref rtTemp, desc, name: "_TemporaryColorTexture");
        RenderingUtils.ReAllocateIfNeeded(ref rtOutlinePass, desc, name: "_OutlinePass");
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, _profilingSampler))
        {

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            cmd.SetRenderTarget(rtOutlinePass);
            cmd.ClearRenderTarget(true, true, Color.clear);
            SortingCriteria sortingCriteria = renderingData.cameraData.defaultOpaqueSortFlags;
            if (rtTemp.rt == null || rtColor.rt == null || blitMaterial == null)
            {
                return;
            }
            DrawingSettings drawingSettings = CreateDrawingSettings(shaderTagsList, ref renderingData, sortingCriteria);
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