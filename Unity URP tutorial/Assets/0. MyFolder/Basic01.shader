// Shader 시작. 셰이더의 폴더와 이름을 여기서 결정합니다.
Shader "URPBasic"
{
    Properties
    {
        // Properties Block : 셰이더에서 사용할 변수를 선언하고 이를 material inspector에 노출시킵니다
        // range 값은 float, color 값은 half 추천
        _Intensity("Range Sample", Range(0,1)) = 1
        _TintColor("Color Sample",Color) = (1,1,1,1)
        _MainTex("RGB(A)",2D) = "white"{}
    }

    SubShader
    {
        Tags
        {
            //Render type과 Render Queue를 여기서 결정합니다.
            "RenderPipeline"="UniversalPipeline"  // URP 선언한다는 뜻
            "RenderType"="Opaque"  // 렌더 타입
            "Queue"="Geometry"  // 그리는 순서
        }
        Pass  // 셰이더에서 사용할 랜더 패스 결정 window > analysis > frame debug 에서 각 패스를 볼 수 있음
        {
            Name "Universal Forward" 
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma vertex vert
            #pragma fragment frag

            //cg shader는 .cginc를 hlsl shader는 .hlsl을 include하게 됩니다.
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            //vertex buffer에서 '읽어올 정보'를 선언합니다. 	
            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //보간기를 통해 버텍스 셰이더에서 픽셀 셰이더로 전달할 정보를 선언합니다.
            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : COLOR;
            };
            
            float _Intensity;
            float4 _TintColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            //버텍스 셰이더 (vertex)
            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);  // 오브젝트 -> 월드 -> 카메라 -> 클립
                o.uv = v.uv.xy;
                o.positionWS = TransformObjectToWorld(v.vertex.xyz);
                
                return o;
            }

            //픽셀 셰이더 (fragment)
            half4 frag(VertexOutput i) : SV_Target
            {
                // 프로퍼티에서 받았던 값을 나타냄
                float2 uv = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
          	    float4 color = tex2D(_MainTex, i.positionWS.xz) * _TintColor * _Intensity;      

                return color;
            }
            ENDHLSL
        }
    }
}