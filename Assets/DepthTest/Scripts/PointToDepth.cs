using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PointToDepth : MonoBehaviour {

	private Camera camera;
	public Material postEffectMat;
	public GameObject depthPlane;
	public GameObject moviePlane;
	public Texture depthTexture;
	public Texture movieTexture;
	public Camera realCamera;
	//public Texture depthMovie;
	public Texture2D depthRead;
	public RenderTexture renderDepth;
	public Vector2 tmpPos;
	public GameObject obj;
	void Start () { 
		camera = GetComponent<Camera>();
		camera.depthTextureMode = DepthTextureMode.Depth;
		depthRead = new Texture2D(512, 256);
		depthTexture = depthPlane.GetComponent<MeshRenderer>().material.mainTexture ;//直接获得plane的,不会受到光照的影响
		movieTexture = realCamera.targetTexture;
		postEffectMat.SetTexture("_DepthTex", depthTexture);
		postEffectMat.SetTexture("_RealTex", movieTexture);
		
	}
	

	void Update () {

		//if(Input.GetMouseButtonDown(1))
		//{
		//	Vector4 a = new Vector4(1, 1, 1, 1);
		//	Matrix4x4 m = new Matrix4x4(new Vector4(2,0,0,0),
		//								new Vector4(0,2,0,0),
		//								new Vector4(0,0,3,0),
		//								new Vector4(0,2,4,1));
		//	Debug.Log(Mul(m, a));
		//}
		//Debug.Log("Screen Point" + Camera.main.WorldToScreenPoint(obj.transform.position));
		if (Input.GetMouseButtonDown(0))
		{

			GetColor(renderDepth);
			if (depthRead != null)
			{
				int scale = depthRead.width/Screen.width;
				//Debug.Log(
				//	depthRead.GetPixel(
				//	(int)(Input.mousePosition.x*scale),
				//	(int)(Input.mousePosition.y*scale))
				//	);
				Color pixel = depthRead.GetPixel(
					(int)(Input.mousePosition.x * scale),
					(int)(Input.mousePosition.y * scale));
				//对rgb中某一个值当成ndc坐标 0-1	
				//ndc -> 摄像机空间 -> world space
				float depth = pixel.b;
				Debug.Log("01Depth: "+ depth+" 非线性转换: "+ Depth01ChangeToUnityDepth(depth));
				depth = Depth01ChangeToUnityDepth(depth);

				Debug.Log("UnityDepth：" + depth + ", 01Depth: " + UnityDepthChangeTo01Depth(depth));
				//depth = 1 - depth;
				//Debug.Log(depth);
				Matrix4x4 currentViewProjectionMatrix = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;//世界空间到视角空间的转换
				Matrix4x4 currentViewProjectionInverseMatrix = currentViewProjectionMatrix.inverse;
				//Debug.Log(currentViewProjectionInverseMatrix);
				//Debug.Log(Camera.main.projectionMatrix);
				//Debug.LogWarning(Camera.main.projectionMatrix.inverse);
				//Debug.Log(currentViewProjectionMatrix);
				
				float ndcX = (Input.mousePosition.x / Screen.width) * 2 - 1;
				float ndcY = (Input.mousePosition.y / Screen.height) * 2 - 1;				
				float ndcZ = depth * 2 - 1;
				//Debug.Log("depth:" + depth+ " ,Input.mousePosition:"+ Input.mousePosition);
				//Vector3 ndc = new Vector4(ndcX,ndcY, ndcZ);

				//Debug.Log("NDC:" + ndc+"WorldPos:" + screenPoint);
				////Debug.Log("NDC:" + ndc);
				//Vector3 viewPortPos = Camera.main.projectionMatrix.inverse.MultiplyPoint(ndc);

				//Vector3 worldPos = currentViewProjectionInverseMatrix.MultiplyPoint(ndc);
				Vector4 ndc = new Vector4(ndcX, ndcY, ndcZ, 1);
				//Debug.Log("projection:"+ Camera.main.projectionMatrix);
				//Debug.Log("total mat:"+ currentViewProjectionInverseMatrix);
				Vector4 worldPos = Mul(currentViewProjectionInverseMatrix, ndc);
				//Debug.Log("no divise w:" + worldPos);
				worldPos /= worldPos.w;

				//Debug.Log("NDC:" + ndc+",  worldPos:"+ worldPos);
				//Vector3 screenPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition + new Vector3(0,0,worldPos.z));
				//Debug.Log("NDC:" + ndc + "WorldPos:" + screenPoint);
				obj.transform.position = new Vector3(worldPos.x,worldPos.y,worldPos.z);
			}
		}


	}
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (postEffectMat == null)
		{
			
			
			Graphics.Blit(source, destination);
		}
		else
		{

			Graphics.Blit(source, destination, postEffectMat);
		}
	}
	Texture2D duplicateTexture(Texture2D source)
	{
		RenderTexture renderTex = RenderTexture.GetTemporary(
					source.width,
					source.height,
					0,
					RenderTextureFormat.Default,
					RenderTextureReadWrite.Linear);

		Graphics.Blit(source, renderTex);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = renderTex;
		Texture2D readableText = new Texture2D(source.width, source.height);
		readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
		readableText.Apply();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary(renderTex);
		return readableText;
	}

	void GetColor(RenderTexture tex)
	{
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = tex;
		depthRead.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
		depthRead.Apply();
		RenderTexture.active = previous;
		
	}
	Vector4 Mul(Matrix4x4 mat,Vector4 ndc)
	{
		Vector4 res = new Vector4(0, 0, 0, 0);

		res.x = (mat.GetRow(0).x * ndc.x) + (mat.GetRow(0).y * ndc.y) + (mat.GetRow(0).z * ndc.z) + (mat.GetRow(0).w * ndc.w);
		res.y = (mat.GetRow(1).x * ndc.x) + (mat.GetRow(1).y * ndc.y) + (mat.GetRow(1).z * ndc.z) + (mat.GetRow(1).w * ndc.w);
		res.z = (mat.GetRow(2).x * ndc.x) + (mat.GetRow(2).y * ndc.y) + (mat.GetRow(2).z * ndc.z) + (mat.GetRow(2).w * ndc.w);
		res.w = (mat.GetRow(3).x * ndc.x) + (mat.GetRow(3).y * ndc.y) + (mat.GetRow(3).z * ndc.z) + (mat.GetRow(3).w * ndc.w);
		return res;
	}

	float Depth01ChangeToUnityDepth(float depth01)
	{
		float res = (Camera.main.nearClipPlane - Camera.main.farClipPlane*depth01)
					/ (depth01 * (Camera.main.nearClipPlane - Camera.main.farClipPlane));
		return res;
	}
	float UnityDepthChangeTo01Depth(float unityDepth)
	{
		float res = Camera.main.nearClipPlane /
			((Camera.main.nearClipPlane - Camera.main.farClipPlane) * unityDepth + Camera.main.farClipPlane);
		return res;
	}
}
