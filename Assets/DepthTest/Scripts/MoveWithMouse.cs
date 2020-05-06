using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour {
 
	public Vector3 point;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.mousePosition);
		//transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition+new Vector3(0,0,point.z));
	}
}
