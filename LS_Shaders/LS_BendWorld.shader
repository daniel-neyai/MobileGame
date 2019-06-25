Shader "LaveraxInteractive/LS_BendWorld"
{
	Properties
	{
		//_MainTex ("Texture", 2D) = "white" {}

		_Curvature("Curvature", Float) = 0.001
		_Shininess("Shininess", Range(0.03, 1)) = 0.078125
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)


	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			//LOD 100
			LOD 250

			CGPROGRAM
			#pragma surface surf Lambert vertex:vert addshadow

			uniform sampler2D _MainTex;
			uniform float _Curvature;
			//$$
			half _Shininess;
			fixed4 _Color;
			//$$
			inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
			{
				fixed diff = max(0, dot(s.Normal, lightDir));
				fixed nh = max(0, dot(s.Normal, halfDir));
				fixed spec = pow(nh, s.Specular * 128) * s.Gloss;

				fixed4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
				UNITY_OPAQUE_ALPHA(c.a);
				return c;
			}

			struct Input
			{
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v) 
			{
				float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
				worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
				worldSpace = float4(0.0f, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);

				v.vertex += mul(unity_WorldToObject, worldSpace);
			}

			void surf(Input IN, inout SurfaceOutput o)
			{/*
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rbg;
				o.Alpha = c.a;*/

				fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = _Color.rgb*tex.rgb;
				o.Gloss = tex.a;
				o.Alpha = tex.a;
				o.Specular = _Shininess;
			}
	ENDCG
	}
	FallBack "Mobile/Diffuse"
}
