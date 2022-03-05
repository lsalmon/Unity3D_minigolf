Shader "Custom/Cell"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }

            CGPROGRAM
                struct appdata
                {
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float3 worldNormal : NORMAL;
                };

                v2f vert (appdata v)
                {
                    v2f o;
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    return o;
                };

            ENDCG

            HLSLPROGRAM

            

            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
