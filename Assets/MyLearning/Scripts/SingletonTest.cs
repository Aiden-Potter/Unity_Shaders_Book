using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTest : MonoBehaviour {
    private static SingletonTest instance;
    //private GameObject obj;
    public static SingletonTest Instance{
        get
        {
            //if(instance==null)
                
            //Debug.Log(obj.name);
            return instance;
        }   

    }
    private SingletonTest() { }
    void Awake()
    {
        Debug.Log(gameObject.name);
        if(instance ==null)
         instance = this;
        //obj = gameObject;
    }
    public void HelloWorld()
    {
        Debug.Log("hello world: " + gameObject.name);
    }
}
