using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State of people
public class PeopleState : MonoBehaviour 
{
	private bool _alive = true;
	public bool Alive
	{
		get
		{
			return _alive;
		}
		set
		{
			_alive = value;
			BroadcastMessage("OnAliveChanged", _alive);
		}
	}
}
