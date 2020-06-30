using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	void Start()
	{
		Dead();
	}
	public void Update()
	{
		
	}

	public void Dead()
	{
		Debug.Log("Monster dead");
		EventCenter.Instance.EventTriger("Monster Defeated!");
	}
	
}
