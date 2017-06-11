using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterMinute : MonoBehaviour {
	private float minutes;
	// Use this for initialization
	void Start () {
		minutes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		minutes += Time.deltaTime;
		if (minutes >= 5) {
			Destroy (gameObject);
		}
	}
}
