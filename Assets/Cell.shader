Shader "Unlit/Cell"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            float3 getNormal (float3 normal, float3 lightDirection)
            {
                float NdotL = dot(normalize(lightDirection), normalize(normal));
                return max(0.0, NdotL);
            }

            struct appdata
            {
                float3 normal : NORMAL;
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // separation band between light part and shadow part
                float NdotL = getNormal(i.worldNormal, _WorldSpaceLightPos0);
                NdotL = smoothstep(0, 0.01, NdotL);

                // use color of directional light to shade
                float4 light = (NdotL * _LightColor0);

                // attenuation so the shadow side isnt entierely black
                float attenuation = 0.3;

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col *= (light + attenuation);
            }
            ENDCG
        }
    }
}
