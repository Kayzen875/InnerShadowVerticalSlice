Shader "IFP/Contraste"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Contraste  ("Contraste", float) = 1
        _Brillo  ("Brillo", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
        LOD 100

        GrabPass
        {
            "_Background" //
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex VShader
            #pragma fragment PShader

            #include "UnityCG.cginc"

            struct VSInput
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct PSInput
            {
                float2 uv : TEXCOORD0;
                float4 backgroundPosition : TEXCOORD1;
                float4 position : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _Background;
            float _Contraste;
            float _Brillo;


            PSInput VShader (VSInput i)
            {
                PSInput o;
                o.position = UnityObjectToClipPos(i.position);
                o.uv = i.uv;
                o.backgroundPosition = ComputeGrabScreenPos(o.position);
                return o;
            }

            fixed4 PShader (PSInput i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // sample the texture

                fixed4 bgCol = tex2Dproj(_Background, i.backgroundPosition);


                bgCol.r = 0.5f + (bgCol.r - 0.5f) * _Contraste;
                bgCol.g = 0.5f + (bgCol.g - 0.5f) * _Contraste;
                bgCol.b = 0.5f + (bgCol.b - 0.5f) * _Contraste;

                if(bgCol.r <= 0.9)
                {
                    bgCol.r += _Brillo;
                }

                if(bgCol.g <= 0.9)
                {
                    bgCol.g += _Brillo;
                }

                if(bgCol.b <= 0.9)
                {
                    bgCol.b += _Brillo;
                }

                return bgCol;
            }
            ENDCG
        }
    }
}
