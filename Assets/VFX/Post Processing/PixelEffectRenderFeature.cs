using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelEffectRenderFeature : ScriptableRendererFeature
{
    public Shader shader;
    private Material material = null;
    public Settings settings;
    private PixelEffectPass pass;
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
        if (material == null || material.shader != shader)
        {
            // only create material if null or different shader has been assigned

            if (material != null) CoreUtils.Destroy(material);
            // destroy material using previous shader

            material = CoreUtils.CreateEngineMaterial(shader);
        }
        pass = new PixelEffectPass(material, settings, name);
        pass.renderPassEvent = _event;
    }
    protected override void Dispose(bool disposing)
    {
        pass.ReleaseTargets();
    }
    [System.Serializable]
    public class Settings
    {
        public Vector4 QuantizationAmounts;
        public int Samples;
        public float DitherSpread;
    }

}
class PixelEffectPass : ScriptableRenderPass
{
    RTHandle rtTemp, rtColor;
    PixelEffectRenderFeature.Settings settings;
    Material blitMaterial;
    private ProfilingSampler _profilingSampler;
    public PixelEffectPass(Material material, PixelEffectRenderFeature.Settings settings, string name)
    {
        this.settings = settings;
        this.profilingSampler = new ProfilingSampler(name);
        blitMaterial = material;
        blitMaterial.SetFloat("_DitherSpread", settings.DitherSpread);
        blitMaterial.SetInt("_SampleAmount", settings.Samples);
        blitMaterial.SetVector("_QuantizationAmounts", settings.QuantizationAmounts);
    }
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
        desc.depthBufferBits = 0;
        RenderingUtils.ReAllocateIfNeeded(ref rtTemp, desc, name: "_TemporaryColorTexture");
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, _profilingSampler))
        {

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            if (rtTemp.rt == null || rtColor.rt == null)
            {
                return;
            }
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
    }
}