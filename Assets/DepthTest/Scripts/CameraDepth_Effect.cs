using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraDepth_Effect : MonoBehaviour {

	private Camera camera;
	public Material postEffectMat;
	public GameObject depthPlane;
	public GameObject moviePlane;
	public Texture depthTexture;
	public Texture movieTexture;
	public Camera realCamera;

	#region 字段
	public Material mat;
	public float velocity = 5;
	private bool isScanning;
	private float dis;
	public Vector3 point;
	#endregion


	void Start () {
		camera = GetComponent<Camera>();
		camera.depthTextureMode = DepthTextureMode.Depth;
		//Debug.Log(moviePlane.GetComponent<MeshRenderer>().material.mainTexture);
		depthTexture = depthPlane.GetComponent<MeshRenderer>().material.mainTexture;//直接获得plane的,不会受到光照的影响
		movieTexture = realCamera.targetTexture;
		postEffectMat.SetTexture("_DepthTex", depthTexture);
		postEffectMat.SetTexture("_RealTex", movieTexture);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log();
		//Debug.Log(realCamera.ScreenToWorldPoint(point));
		//postEffectMat.SetTexture("_DepthTex", depthTexture);
		//Debug.Log("Depth："+postEffectMat.GetFloat("_Z"));
		if (this.isScanning)
		{
			this.dis += Time.deltaTime * this.velocity;
		}

		//按c开启扫描
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.isScanning = true;
			this.dis = 0;
		}
	}
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (postEffectMat == null)
		{
			mat.SetFloat("_ScanDistance", dis);
			
			Graphics.Blit(source, destination);
		}
		else
		{
			mat.SetFloat("_ScanDistance", dis);
			Graphics.Blit(source, destination, postEffectMat);
		}
	}

}
