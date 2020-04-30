using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextrueTransfer : MonoBehaviour {

	public Texture2D tex;
	public Texture2D shape;
	public Vector2 point;
	void Start () {
		//tex = new Texture2D(512, 256, TextureFormat.ARGB32, false);
		//for(int x=0;x<512;++x)
		//{
		//	for (int y = 0; y < 256; ++y)
		//	{
		//		float _x = x / 512f;
		//		float _y = y / 256f;
		//		Color col = new Color(_x, _y, 0, 1) * shape.GetPixelBilinear(1-_x, 1-_y).r;
		//		tex.SetPixel(x, y,col);//color的范围都是0,1
		//	}
		//}
		////从左下角到右上角排列
		//tex.Apply();//像素的set是放到一块临时空间里，apply把这里面的东西放到显存
		//GetComponent<Renderer>().material.mainTexture = tex;//texture2d继承texture
		
	}

	void Update () {
		tex = GetComponent<Renderer>().material.mainTexture as Texture2D;
		Debug.Log(tex.GetPixel((int)point.x, (int)point.y));//512 256
	}
}
