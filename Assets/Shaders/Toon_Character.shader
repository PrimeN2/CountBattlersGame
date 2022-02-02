Shader "Custom/Toon_Character"
{

	Properties
	{
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull mode", Float) = 2
		[Header(ToonColor)]
		_Color("Main Color", Color) = (0.5, 0.5, 0.5, 1)
		_HColor("Highlight Color", Color) = (0.785,0.785,0.785,1.0)
		_SColor("Shadow Color", Color) = (0.195,0.195,0.195,1.0)
		_Threshold("Ramp Threshold", Range(0,1)) = 0.5
		_Smooth("Ramp Smoothing", Range(0.01,1)) = 0.1
		[Header(Textures)]
		_MainTex("Base (RGB)", 2D) = "white" {}
	
		[Header(SetupOption)]
		[Toggle] _DamageOnOff("Damage On/Off", Float) = 0
		_Amount("Height Adjustment", Range(0, 0.5)) = 0
		_DamageColor("DamageColor", Color) = (0, 0, 0, 1)
		_DamagePower("DamagePower", Range(0, 10)) = 10
		
		[Header(Outline)]
		[MaterialToggle]_USEOUTLINE("Outline On/Off", Int) = 0
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineWidth("Outline Width", Range(0, 0.1)) = 0

	}

		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			Cull[_Cull]
			LOD 200
			
			Stencil 
			{
				Ref 1
				Comp Always
				Pass Replace
			}

			CGPROGRAM
			#pragma surface surf ToonRamp addshadow vertex:vert
			#pragma target 3.0

			uniform sampler2D _MainTex;
			uniform half4 _Color, _HColor, _SColor, _DamageColor;
			uniform half _DamagePower, _DamageOnOff, _Threshold, _Smooth, _Amount;


			inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
				#ifndef USING_DIRECTIONAL_LIGHT
				lightDir = normalize(lightDir);
				#endif

				half nld = max(0, dot(s.Normal, lightDir) * 0.5 + 0.5);
				half3 ramp = smoothstep(_Threshold - _Smooth * 0.5, _Threshold + _Smooth * 0.5, nld);

				half4 c;
				_SColor = lerp(_HColor, _SColor, _SColor.a);
				ramp = lerp(_SColor.rgb, _HColor.rgb, ramp);
				c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten ;
				c.a = s.Alpha;
				return c;
			}

			struct Input {
				half2 uv_MainTex;
				float2 uv_DissTexture;
				float2 uv_MaskTex;
				float3 viewDir;
				float3 normal;
			};


			void vert(inout appdata_full v) {
				
				v.vertex.xyz += v.normal * _Amount * _DamageOnOff;
			}
			
			void surf(Input IN, inout SurfaceOutput o) {
				
				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				rim = pow(rim,_DamagePower);
				half4 basecol = tex2D(_MainTex, IN.uv_MainTex) * _Color;

				o.Albedo = basecol;
				o.Emission = rim * _DamageColor * _DamageOnOff;
				o.Alpha = basecol.a;
			}

			ENDCG

			Pass{

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front
			//ZWrite On
			
			//When using only borders!!!
			/*
			Stencil 
			{
				Ref 1
				Comp Greater
			}
			*/

			CGPROGRAM
			#pragma shader_feature _USEOUTLINE_OFF _USEOUTLINE_ON
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
				uniform half4 _OutlineColor;
				uniform float _OutlineWidth;

			struct vertexInput {
			 	float4 pos: POSITION;
			 	float4 texcoord: TEXCOORD0;
			};

			struct vertexOutput {
				float4 pos: SV_POSITION;
			 	float4 texcoord: TEXCOORD0;
			};

			float4 Outline(float4 vertPos, float width)
			{
				float4x4 scaleMat;
				scaleMat[0][0] = 1.0 + width;
				scaleMat[0][1] = 0.0;
				scaleMat[0][2] = 0.0;
				scaleMat[0][3] = 0.0;
				scaleMat[1][0] = 0.0;
				scaleMat[1][1] = 1.0 + width;
				scaleMat[1][2] = 0.0;
				scaleMat[1][3] = 0.0;
				scaleMat[2][0] = 0.0;
				scaleMat[2][1] = 0.0;
				scaleMat[2][2] = 1.0 + width;
				scaleMat[2][3] = 0.0;
				scaleMat[3][0] = 0.0;
				scaleMat[3][1] = 0.0;
				scaleMat[3][2] = 0.0;
				scaleMat[3][3] = 1.0;	

				return mul(scaleMat, vertPos);
			}

			vertexOutput vert(vertexInput v) 
			{
					vertexOutput o;
			#if _USEOUTLINE_ON
					o.pos = UnityObjectToClipPos(Outline(v.pos,_OutlineWidth));
			#elif _USEOUTLINE_OFF
					o.pos = float4(0, 0, 0, 0);
			#endif
					return o;
			}

			half4 frag(vertexOutput i): COLOR 
			{
			 	return _OutlineColor;
			}
			ENDCG
			}
		}
	Fallback "Diffuse"
}