Shader "Custom/BloodEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BloodIntensity ("Blood Intensity", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BloodIntensity;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                if (color.r < 1.0)
                {
                    float bloodAmount = 1.0 - color.r;

                    float bloodIntensity = _BloodIntensity * (1.0 - bloodAmount);
                    color.rgb += float3(bloodIntensity, 0, 0);
                }

                return color;
            }
            ENDCG
        }
    }
}