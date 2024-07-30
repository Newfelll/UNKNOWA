Shader "Custom/TVStaticShader"
{
    Properties
    {
        _MainTex ("Image", 2D) = "white" {}
        _Blend ("Blend", Range(0,1)) = 0.5
        _Speed ("Speed", Range(0, 10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Blend;
            float _Speed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float random (float2 st)
            {
                return frac(sin(dot(st.xy, float2(12.9898,78.233))) * 43758.5453123);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float noise = random(i.uv * (_Time.y * _Speed) * 100.0);
                fixed4 staticColor = fixed4(noise, noise, noise, 1.0);
                fixed4 imageColor = tex2D(_MainTex, i.uv);
                return lerp(imageColor, staticColor, _Blend);
            }
            ENDCG
        }
    }
}
