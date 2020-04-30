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
		if(Input.GetMouseButtonDown(0))
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
				float depth = pixel.r; 
				Matrix4x4 currentViewProjectionMatrix = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;//世界空间到视角空间的转换
				Matrix4x4 currentViewProjectionInverseMatrix = currentViewProjectionMatrix.inverse;
				//Debug.Log(currentViewProjectionInverseMatrix);
				//Debug.Log(Camera.main.projectionMatrix);
				//Debug.LogWarning(Camera.main.projectionMatrix.inverse);
				//Debug.Log(currentViewProjectionMatrix);
				Debug.Log(Input.mousePosition);
				float ndcX = (Input.mousePosition.x / Screen.width) * 2 - 1;
				float ndcY = (Input.mousePosition.y / Screen.height) * 2 - 1;				
				float ndcZ = depth * 2 - 1;
		
				Vector3 ndc = new Vector4(ndcX,ndcY, ndcZ);
				
				//Debug.Log("NDC:" + ndc+"WorldPos:" + screenPoint);
				////Debug.Log("NDC:" + ndc);
				Vector3 viewPortPos = Camera.main.projectionMatrix.inverse.MultiplyPoint(ndc);
	
				Vector3 worldPos = currentViewProjectionInverseMatrix.MultiplyPoint(ndc);
				Debug.Log("NDC:" + ndc+", viewPortPos:"+viewPortPos+", worldPos:"+ worldPos);
				//Vector3 screenPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition + new Vector3(0,0,worldPos.z));
				//Debug.Log("NDC:" + ndc + "WorldPos:" + screenPoint);
				obj.transform.position = worldPos;

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
}
