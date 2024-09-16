using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RadialBlurRenderFeature : ScriptableRendererFeature
{
    public RadialBlurPass radialBlurPass;
    public RenderPassEvent renderPassEvent;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(radialBlurPass);
    }

    public override void Create()
    {
        radialBlurPass = new RadialBlurPass(renderPassEvent);
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        radialBlurPass.ReleaseTargets();
    }
    public void OnDestroy()
    {
        radialBlurPass.ReleaseTargets();
    }
}

[System.Serializable]
public class RadialBlurPass : ScriptableRenderPass
{
    ProfilingSampler _profilingSampler;
    RTHandle tempRT;
    Material _blitMat;
    public RadialBlurPass(RenderPassEvent renderPassEvent)
    {
        this.renderPassEvent = renderPassEvent;
    }
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor colorPassDesc = renderingData.cameraData.cameraTargetDescriptor;
        colorPassDesc.depthBufferBits = 0;
        RenderingUtils.ReAllocateIfNeeded(ref tempRT, colorPassDesc, name: "_TemporaryColorTexture");

    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var stack = VolumeManager.instance.stack;
        var radialBlurVolumeComponent = stack.GetComponent<RadialBlurVolumeComponent>();
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, _profilingSampler))
        {
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            _blitMat = radialBlurVolumeComponent.radialBlurMaterial.value;
            if (radialBlurVolumeComponent != null && radialBlurVolumeComponent.active && _blitMat != null)
            {
                _blitMat.SetFloat("_EffectAmount", radialBlurVolumeComponent.EffectAmount.value);
                _blitMat.SetInt("_SampleAmount", radialBlurVolumeComponent.SampleAmount.value);
                RTHandle camTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;
                if (camTarget != null && tempRT != null  && camTarget != null && tempRT.rt != null && camTarget.rt != null)
                {
                    Blitter.BlitCameraTexture(cmd, camTarget, tempRT, _blitMat, 0);
                    Blitter.BlitCameraTexture(cmd, tempRT, camTarget);
                }
            }
        }
        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();
        CommandBufferPool.Release(cmd);
    }
    public void ReleaseTargets()
    {
        tempRT?.Release();
    }
}
