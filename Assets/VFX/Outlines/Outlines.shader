Shader "Custom/Outline"
{
    Properties
    {
        _OutlinesPass("Outlines Pass", 2D) = ""{}
        _KernelSize("Kernel Size", float) = 10
    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        Texture2D _OutlinesPass;
        float _KernelSize;
        sampler sampler_BlitTexture_Trilinear_Clamp, sampler_OutlinesPass_Trilinear_Clamp;
        float4 frag (Varyings i) : SV_Target
        {
            float4 col = 0;
            int count = 0;
            for(float j = -_KernelSize; j <= _KernelSize; j++)
            {
                for(float k = -_KernelSize; k <= _KernelSize; k++)
                {
                    col += _OutlinesPass.Sample(sampler_OutlinesPass_Trilinear_Clamp, i.texcoord + float2(j *(1/_ScreenParams.x) ,k *(1/_ScreenParams.y)));
                    count++;
                }
            }
            col /= count;
            col -= _OutlinesPass.Sample(sampler_OutlinesPass_Trilinear_Clamp, i.texcoord);
            float4 blitCol = _BlitTexture.Sample(sampler_BlitTexture_Trilinear_Clamp, i.texcoord);
            return lerp(blitCol, col, col.a);
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