// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/DepthWrite"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_DepthTex("DepthTex", 2D) = "white" {}
		_RealTex("RealTex",2D) = "white"{}
		_Threshold("Threshold",Range(0,0.2)) = 0.1
		//_ZTex("ZTex",2D) = "white"{}
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
			
		Pass
		{
			ZTest Off
			ZWrite On

			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
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
			#pragma vertex vert_write//自带的顶点着色器
			#pragma fragment frag_write
			sampler2D _DepthTex;
			float4 _DepthTex_ST;

			#include "UnityCG.cginc"

			v2f vert_write(a2v i)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = i.texcoord0;

				return o;
			}

			fixed frag_write(v2f i) : SV_Depth
			{
				fixed depth = tex2D(_DepthTex, i.uv).r;
				float depthZ = LinearEyeDepth(depth);
				return depth;
			}

			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			ZTest On
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
			sampler2D _RealTex;
			float4 _RealTex_ST;
			float _Threshold;
			//sampler2D _ZTex;
			//float4 _ZTex_ST;
			//float _Z;
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

				fixed linear01EyeDepth = Linear01Depth(depthTextureValue);
				fixed depth = tex2D(_DepthTex, i.uv).r;
				float  depthEye = LinearEyeDepth(depth);
				//_Z = depthEye;
				fixed3 mainTex = tex2D(_MainTex, i.uv).rgb;
				fixed3 realTex = tex2D(_RealTex, i.uv).rgb;
				//clip(linear01EyeDepth-depth );
				//clip(depth - linear01EyeDepth-_Threshold);


				if (depth - linear01EyeDepth < _Threshold)
					 return fixed4(realTex,1.0);
				//return fixed4(linear01EyeDepth, linear01EyeDepth, linear01EyeDepth, 1.0);
				return fixed4(mainTex, 1.0);
			}

			ENDCG
		}
	}
}
