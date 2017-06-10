using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomFlip : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		GetComponent<SpriteRenderer>().flipX = Random.value < 0.5;
	}
}
