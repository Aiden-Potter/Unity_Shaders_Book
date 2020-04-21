// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/DepthLearning"
{
	Properties
	{
		_MainTex ("Texture1", 2D) = "white" {}
		_DepthTex("Texture2", 2D) = "white" {}
		_Threshold("Threshold",Range(0,0.2)) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			ZTest Off
			Cull Off
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert//自带的顶点着色器
			#pragma fragment frag
			sampler2D _CameraDepthTexture;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DepthTex;
			float4 _DepthTex_ST;
			float _Threshold;
			#include "UnityCG.cginc"
			struct a2v {
				float4 vertex : POSITION;
				float2  texcoord0 : TEXCOORD0;
				//float2 texcoord1 : TEXCOORD1;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				//float2 uv2 :TEXCOORD1;
			};
			v2f vert(a2v i)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv =  i.texcoord0;
				//o.uv2 = TRANSFORM_TEX(v.texcoord1, _MainTex);;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float depthTextureValue = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
				//这个uv到底是啥？自带的uv？

				fixed linear01EyeDepth = Linear01Depth(depthTextureValue);
				fixed depth = tex2D(_DepthTex, i.uv).r;
				fixed3 tex = tex2D(_MainTex, i.uv).rgb;
				//clip(linear01EyeDepth-depth );
				//clip(depth - linear01EyeDepth-_Threshold);
				if (depth - linear01EyeDepth< _Threshold)
					 return fixed4(0.4,0.4,0.4, 1.0);
				//return fixed4(linear01EyeDepth, linear01EyeDepth, linear01EyeDepth, 1.0);
				return fixed4(tex, 1.0);
			}

			ENDCG
		}
	}
}
