using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YtoSortingOrder : MonoBehaviour
{
	SpriteRenderer sprite;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update ()
	{
		int pos = Mathf.RoundToInt(transform.position.y);
		pos /= 3;
		sprite.sortingOrder = -pos;
	}
}
