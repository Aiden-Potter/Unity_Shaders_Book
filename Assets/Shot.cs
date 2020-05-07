using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {



	void Update()
	{
		if(Input.GetMouseButton(0))
		{
			PoolMgr.Instance.GetObj("Cube");
		}
		if(Input.GetMouseButton(1))
		{
			PoolMgr.Instance.GetObj("Sphere");
		}
	}
}
