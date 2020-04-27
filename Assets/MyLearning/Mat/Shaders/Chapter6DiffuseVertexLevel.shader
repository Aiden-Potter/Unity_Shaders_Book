// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/Chaper 6/Diffuse Vertex-Level-self"
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
				float3 color : COLOR;
				float4 pos : SV_POSITION;
			};


			v2f vert (a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);//顶点空间到裁剪空间
				//Get ambient term环境光内置变量
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

				//Transform the normal from obj space to world space
				fixed3 worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));
				//Get the light Dir，指向光源的单位矢量？光源的Pos，只适用于平行光
				fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));
				
				o.color = ambient + diffuse;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				return fixed4(i.color,1.0);
			}
			ENDCG
		}
	}
		FallBack "Diffuse"
}
