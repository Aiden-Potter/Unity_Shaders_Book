using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : BaseManager<EventCenter>
{
	//key event name
	//value delegate
	private Dictionary<string, UnityAction> eventDic = new Dictionary<string, UnityAction>();
	//无参数无返回值
	public void AddEventListener(string _name, UnityAction _action)
	{
		if (eventDic.ContainsKey(_name))
		{
			eventDic[_name] += _action;
		}
		else
		{
			eventDic.Add(_name, _action);
		}
	}
	public void EventTriger(string _name)
	{
		if(eventDic.ContainsKey(_name))
		{
			eventDic[_name].Invoke();
		}
	}
	public void RemoveListener(string _name,UnityAction _action)
	{	
		if(eventDic.ContainsKey(_name))
		{
			eventDic[_name] -= _action;
		}
	}
	public void Clear()
	{
		eventDic.Clear();
	}
}
