using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraDepthTest : MonoBehaviour {

	private Camera camera;
	public Material postEffectMat;
	public GameObject moviePlane;
	public Texture depthTexture;

	void Start () {
		camera = GetComponent<Camera>();
		camera.depthTextureMode = DepthTextureMode.Depth;
		depthTexture = moviePlane.GetComponent<MeshRenderer>().material.mainTexture;
		postEffectMat.SetTexture("_DepthTex", depthTexture);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(camera.depthTextureMode.ToString());
		//postEffectMat.SetTexture("_DepthTex", depthTexture);
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

}
