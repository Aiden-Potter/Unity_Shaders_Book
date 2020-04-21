// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Chapter6/Diffuse-Pixel Level-self"
{
	Properties
	{
		_Diffuse("Diffuse",Color) = (1,1,1,1)
	}
		SubShader
	{
		Tags { "LightMode" = "ForwardBase" }//指明光照模式
		//定义了光照模式才可以获得正确的内置光照变量

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			fixed4 _Diffuse;

			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float3 worldNormal : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			v2f vert (a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				fixed3 worldNormal = normalize(i.worldNormal);

				fixed diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal,worldLightDir ));
				fixed3 color = ambient + diffuse;

				return fixed4(color,1);
			}
			ENDCG
		}
	}
}
