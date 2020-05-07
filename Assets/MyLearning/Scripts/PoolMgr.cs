using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
	public GameObject fatherObj;
	public List<GameObject> pool;

	public PoolData(GameObject _obj,GameObject poolObj)
	{
		fatherObj = new GameObject(_obj.name);//emtpty manager obj
		fatherObj.transform.parent = poolObj.transform;
		pool = new List<GameObject>();
		PushObj(_obj);
	}


	public void PushObj(GameObject _obj)
	{
		_obj.transform.SetParent(fatherObj.transform);
		_obj.SetActive(false);
		pool.Add(_obj);

	}
	public GameObject GetObj()
	{
		GameObject obj = null;

		obj = pool[0];
		obj.SetActive(true);
		pool.RemoveAt(0);
		obj.transform.parent = null;
		
		return obj;

	}
}


//未继承Mono的单例对象池
public class PoolMgr : BaseManager<PoolMgr> {

	public static Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
	public GameObject poolObj;

	public  GameObject GetObj(string name)
	{
		GameObject obj=null;
		if (poolDic.ContainsKey(name)&&poolDic[name].pool.Count>0)
		{
			obj = poolDic[name].GetObj();
		}
		else
		{
			obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
			obj.name = name;
		}
		
		return obj;
	}

	public void PushObj(string name, GameObject _obj)
	{
		if (poolObj == null)
			poolObj = new GameObject("Pool");
		
		if (poolDic.ContainsKey(name))
		{
			poolDic[name].PushObj(_obj);
		}
		else
		{
			poolDic.Add(name, new PoolData(_obj,poolObj));
			
		}

	}

	public void Clear(){
		poolDic.Clear();
		poolObj = null;
	}
}
