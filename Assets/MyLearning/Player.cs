using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestCor{ 
	public TestCor()
	{
		MonoMgr.Instance.StartCoroutine(Test123());
	}
	IEnumerator Test123()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("123Coroutine");
	}
	public void Update()
	{
		Debug.Log("TestCor");
	}
}


public class Player : MonoBehaviour {

	TestCor test;
	// Use this for initialization
	void Start () {
		EventCenter.Instance.AddEventListener("Monster Defeated!", DoSomething);
		test = new TestCor();
		MonoMgr.Instance.AddUpdateListener(test.Update);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DoSomething()
	{
		Debug.Log("Player:Wow! Monster was defeated!");
	}
}
