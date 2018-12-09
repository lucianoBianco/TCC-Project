Shader "Custom/DirtShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Activation("Activator", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _Activation;


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 uv = IN.uv_MainTex;
			fixed4 c = tex2D (_MainTex, uv);
			//if(uv.y > 0.5 && uv.x > 0.5){
			o.Albedo = _Color + ((c.rgb * _Time.y / 120) * 0.25);
			/*float pos = ((1 - (length(uv.y - 0.5))) * (1 - (length(uv.x - 0.5)))) *_Activation * 2;
			if(pos > 0.5){
				o.Albedo = c.rgb;
			}
			else if(pos > 0.494){
				o.Emission = c.rgb * 5;
			}
			else if(pos > 0.49){
				o.Emission = 1;
			}
			else{
				o.Albedo = 0;
			}*/
			
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
