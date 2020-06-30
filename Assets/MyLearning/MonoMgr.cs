using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

public class MonoMgr : BaseManager<MonoMgr> {

	public MonoController controller;

	public MonoMgr()
	{
		GameObject obj = new GameObject("MonoController");//单例创建一次
		controller = obj.AddComponent<MonoController>();
	}

	//通过MonoMgr调用MonoController
	public void AddUpdateListener(UnityAction fun)
	{
		controller.AddUpdateListener(fun);
	}
	//给外部添加帧更新的方法
	public void RemoveUpdateListener(UnityAction fun)
	{
		controller.RemoveUpdateListener(fun);
	}

	public Coroutine StartCoroutine(IEnumerator routine)
	{
		return controller.StartCoroutine(routine);
	}
	public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
	{
		return controller.StartCoroutine(methodName, value);
	}
}
