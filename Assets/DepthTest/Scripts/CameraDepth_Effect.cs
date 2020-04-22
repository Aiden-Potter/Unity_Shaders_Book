using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraDepth_Effect : MonoBehaviour {

	private Camera camera;
	public Material postEffectMat;
	public GameObject moviePlane;
	public Texture depthTexture;

	#region 字段

	public Material mat;
	public float velocity = 5;
	private bool isScanning;
	private float dis;

	#endregion


	void Start () {
		camera = GetComponent<Camera>();
		camera.depthTextureMode = DepthTextureMode.Depth;
		//Debug.Log(moviePlane.GetComponent<MeshRenderer>().material.mainTexture);
		depthTexture = moviePlane.GetComponent<MeshRenderer>().material.mainTexture;
		//postEffectMat.SetTexture("_DepthTex", depthTexture); 
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(camera.depthTextureMode.ToString());
		//postEffectMat.SetTexture("_DepthTex", depthTexture);

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
