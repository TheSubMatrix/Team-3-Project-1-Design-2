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
    public NoInterpIntParameter SampleAmount = new NoInterpIntParameter(100);
    public ClampedFloatParameter EffectAmount = new ClampedFloatParameter(value: 0.5f, min: 0, max: 1);


    public bool IsActive() => SampleAmount.value > 1 && EffectAmount.value > 0 && radialBlurMaterial != null;

    public bool IsTileCompatible() => true;
}
