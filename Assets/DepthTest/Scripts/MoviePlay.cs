using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviePlay : MonoBehaviour {

	public Material mat;
	public MovieTexture movieTexture;
	void Awake () {
		mat = GetComponent<MeshRenderer>().material;
		//设置当前对象的主纹理为电影纹理
		mat.mainTexture = movieTexture;
		//设置电影纹理播放模式为循环
		movieTexture.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			mat.mainTexture = movieTexture;
			Debug.Log("Switch");
			if(movieTexture.isPlaying)
			{
				movieTexture.Stop();
			}
			else
			{
				movieTexture.Play();
			}
		}
	}
}
