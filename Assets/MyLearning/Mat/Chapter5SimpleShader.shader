// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Chapter5SimpleShader"
{
	Properties{
		_Color("Color Tint", Color) = (1, 1, 1, 1)
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			//#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag//编译指令
			//哪个函数包括了顶点着色器的代码
			//哪个函数包括了片元着色器的代码
			uniform fixed4 _Color;//前面属性有了，后面CG代码还有定义相匹配的
			struct a2v {
				float4 vertex :POSITION;
				float3 normal:NORMAL;
				float4 texcoord:TEXCOORD0;
			};
			
			struct v2f {
				float4 pos:SV_POSITION;
				fixed3 color : COLOR0;
			};
			v2f vert(a2v v){
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//normal法线方向，分量范围在（-1,1,）
				o.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5);
				return o;
			}//接受顶点坐标（POSITION）返回了顶点在裁剪坐标的位置（SV_POSITION）
			float4 frag(v2f i) : SV_Target{
				fixed3 c = i.color;
				c *= _Color.rgb;
				return fixed4(c ,1.0);
			}//SV_Target告诉渲染器把用户的输出颜色存储到一个渲染目标中，这里是帧缓存
			ENDCG
		}
	}
}
