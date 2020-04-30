using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Mat4x4
{
	public Vector4 x;
	public Vector4 y;
	public Vector4 z;
	public Vector4 w;


	public M_Mat4x4(Matrix4x4 mat)
	{
		x = mat.GetRow(0);
	
	}
	

}
