Shader "Custom/Grass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GrassMask ("Grass Mask", 2D) = "white" {}
        _GrassMaskThreshold("Grass Threshold", Range(0,1)) = 0.9
        _BendRotationRandom("Bend Rotation Random", Range(0, 1)) = 0.05
        _BladeWidth("Blade Average Width", float) = 0.02
        _BladeWidthDeviation("Blade Width Random Deviation", float) = 0.5
        _BladeHeight("Blade Average Height", float) = 0.2
        _BladeHeightDeviation("Blade Height Random Deviation", float) = 0.3
        _BladeForwardAmount("Blade Forward Amount", float) = 0.38
        _BladeCurveAmount("Blade Curvature Amount", Range(1, 4)) = 2
        _TessellationUniform("Tessellation Uniform", Range(1, 64)) = 1
    }
    CGINCLUDE

    ENDCG
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off
        HLSLINCLUDE

            #define BLADE_SEGMENTS 3
            #define UNITY_PI            3.14159265359f
            #define UNITY_TWO_PI        6.28318530718f

            #include "CustomTessellation.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            inline float4 UnityObjectToClipPos(in float3 pos)
            {
            #if defined(STEREO_CUBEMAP_RENDER_ON)
                return UnityObjectToClipPosODS(pos);
            #else
                // More efficient than computing M*VP matrix product
                return mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, float4(pos, 1.0)));
            #endif
            }
            struct geometryOutput
            {
	            float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            float3 ApplyScaleToPosition(float3 pos)
            {
                float3 scale = float3(
                    length(unity_ObjectToWorld._m00_m10_m20),
                    length(unity_ObjectToWorld._m01_m11_m21),
                    length(unity_ObjectToWorld._m02_m12_m22)
                );
                return pos / scale;
            }
            float rand(float3 co)
	        {
		        return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
	        }
            float3x3 AngleAxis3x3(float angle, float3 axis)
	        {
		        float c, s;
		        sincos(angle, s, c);

		        float t = 1 - c;
		        float x = axis.x;
		        float y = axis.y;
		        float z = axis.z;

		        return float3x3(
			        t * x * x + c, t * x * y - s * z, t * x * z + s * y,
			        t * x * y + s * z, t * y * y + c, t * y * z - s * x,
			        t * x * z - s * y, t * y * z + s * x, t * z * z + c
			        );
	        }
            geometryOutput ConvertToGeometryOutput(float3 pos, float2 uvs)
            {
                geometryOutput o;
                o.pos = UnityObjectToClipPos(pos);
                o.uv = uvs;
                return o;
            }
            geometryOutput GenerateGrassVertex(float3 vertexPosition, float width, float height, float forward, float2 uv, float3x3 transformMatrix)
            {
	            float3 tangentPoint = ApplyScaleToPosition(float3(width, forward, height));
	            float3 localPosition = vertexPosition + mul(transformMatrix, tangentPoint);
	            return ConvertToGeometryOutput(localPosition, uv);
            }
            Texture2D _GrassMask;
            sampler sampler_GrassMask;
            sampler2D _MainTex;
            float4 _MainTex_ST, _GrassMask_ST;
            float _BendRotationRandom, _BladeWidth, _BladeWidthDeviation, _BladeHeight, _BladeHeightDeviation, _BladeForwardAmount, _BladeCurveAmount, _GrassMaskThreshold;
            [maxvertexcount(BLADE_SEGMENTS * 2 + 1 + 3)]
            void geo(triangle vertexOutput IN[3] : SV_POSITION, inout TriangleStream<geometryOutput> triStream)
            {
                float2 tiledUVs = IN[0].uv * _GrassMask_ST.xy + _GrassMask_ST.zw;
                float maskVal = _GrassMask.SampleLevel(sampler_GrassMask, tiledUVs, 0);
                float4 pos = IN[0].vertex;
                float3 normal = IN[0].normal;
                float4 tangent = IN[0].tangent;
                float3 binormal = cross(normal, tangent.xyz) * tangent.w;
                float3x3 tangentToLocal = float3x3
                (
	                tangent.x, binormal.x, normal.x,
	                tangent.y, binormal.y, normal.y,
	                tangent.z, binormal.z, normal.z
	            );
                float3x3 facingRotationMatrix = AngleAxis3x3(rand(pos.xyz) * UNITY_TWO_PI, float3(0, 0, 1));
                float3x3 bendRotationMatrix = AngleAxis3x3(rand(pos.zzx) * _BendRotationRandom * UNITY_PI * 0.5, float3(-1, 0, 0));
                float3x3 transformationMatrix = mul(mul(tangentToLocal, facingRotationMatrix),bendRotationMatrix);
                float height = (rand(pos.zyx) * 2 - 1) * _BladeHeightDeviation + _BladeHeight;
                float width = (rand(pos.xzy) * 2 - 1) * _BladeWidthDeviation + _BladeWidth;
                float curveAmount = rand(pos.yyz) * _BladeForwardAmount;
                if(maskVal >= _GrassMaskThreshold)
                {
                for (int i = 0; i < BLADE_SEGMENTS; i++)
                {
	                float t = i / (float)BLADE_SEGMENTS;
                    float segmentForward = pow(t, _BladeCurveAmount) * curveAmount;
                    float segmentHeight = height * t;
                    float segmentWidth = width * (1 - t);
                    triStream.Append(GenerateGrassVertex(pos.xyz, segmentWidth, segmentHeight, segmentForward, float2(0, t), transformationMatrix));
                    triStream.Append(GenerateGrassVertex(pos.xyz, -segmentWidth, segmentHeight, segmentForward, float2(1, t), transformationMatrix));
                }
                }
                triStream.Append(GenerateGrassVertex(pos.xyz, 0, height, curveAmount, float2(0.5, 1), transformationMatrix));            
            }
            void InitializeSurfaceData(geometryOutput i, out SurfaceData surfaceData)
            {
                surfaceData = (SurfaceData)0;
            }
        ENDHLSL
        Pass
        {
            HLSLPROGRAM
            #pragma require geometry
            #pragma require tessellation tessHW
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
            #pragma hull hull
            #pragma domain domain

            #pragma multi_compile_shadowcaster

            float4 frag(geometryOutput i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
	        Tags
	        {
		        "LightMode" = "ShadowCaster"
	        }

	        HLSLPROGRAM
	        #pragma vertex vert
	        #pragma geometry geo
	        #pragma fragment ShadowPassFragment
	        #pragma hull hull
	        #pragma domain domain

	        ENDHLSL
        }
    }
}
