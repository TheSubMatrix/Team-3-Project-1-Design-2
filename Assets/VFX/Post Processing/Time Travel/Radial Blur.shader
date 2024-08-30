Shader "Custom/Radial Blur"
{
    Properties
    {
        _SampleAmount("Sample Amount", int) = 100
        _EffectAmount("Effect Amount", float) = 0.5

        _Radius("Radius", float) = 0.5
    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        int _SampleAmount;
        float _EffectAmount, _Radius;
        sampler sampler_BlitTexture;
        float4 frag (Varyings i) : SV_Target
        {
            float4 col = float4(0,0,0,0);
            float2 dist = i.texcoord - float2(0.5, 0.5);
            for(int j = 0; j < _SampleAmount; j++)
            {
                float scale = 1 - _EffectAmount * (j / (float)_SampleAmount)* (saturate(length(dist) / _Radius));
                col += _BlitTexture.Sample(sampler_BlitTexture, dist * scale + float2(0.5, 0.5));
            }
            return col / _SampleAmount;
        }

    ENDHLSL

    SubShader
    {
        Tags{ "RenderPipeline" = "UniveralPipeline"}
        ZWrite Off ZTest Always Blend Off Cull Off
        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag
            ENDHLSL
        }
    }
}