using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour {

	//生命周期
	//事件
	//协程

	private event UnityAction updateEvent;

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		if (updateEvent!=null)
		{
			updateEvent();
		}
	}

	public void AddUpdateListener(UnityAction fun)
	{
		updateEvent += fun;
	}

	public void RemoveUpdateListener(UnityAction fun)
	{
		updateEvent -= fun;
	}


}
