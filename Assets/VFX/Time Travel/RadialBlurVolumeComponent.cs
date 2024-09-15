using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/Radial Blur", typeof(UniversalRenderPipeline))]
public class RadialBlurVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public MaterialParameter radialBlurMaterial = new MaterialParameter(null);
    public IntParameter SampleAmount = new IntParameter(100);
    public FloatParameter EffectAmount = new FloatParameter(0.5f);

    public bool IsActive() => SampleAmount.value > 1 && EffectAmount.value > 0 && radialBlurMaterial != null;

    public bool IsTileCompatible() => true;
}
