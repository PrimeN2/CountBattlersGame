Shader "Custom/Alpha Mask" {

    Properties {
            _Color ("Color", Color) = (1,1,1,1)
            _MainTex ("Albedo (RGB)", 2D) = "white" {}
            _Mask ("Mask (A)", 2D) = "white" {}
    }
    SubShader 
    {
        Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200
     
        CGPROGRAM
            #pragma surface surf Lambert fullforwardshadows addshadow alpha
            #pragma target 3.0
        
            sampler2D _MainTex;
            sampler2D _Mask;
        
            struct Input {
                float2 uv_MainTex;
                float2 uv_Mask;
            };
            
            fixed4 _Color;
            
            void surf (Input IN, inout SurfaceOutput o) {

                fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Emission = c.rgb;
                o.Alpha = tex2D (_Mask, IN.uv_Mask).a;
            }
        ENDCG
    }
    FallBack "Diffuse"
}
