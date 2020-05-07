using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour {

	void OnEnable()
	{
		Invoke("Push", 1f);
	}

	void Push()
	{
		PoolMgr.Instance.PushObj(gameObject.name, this.gameObject);
	}
}
