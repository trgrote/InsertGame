using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flip the X of the Sprite based off current x velocity
[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class PeopleVelocityFlip : MonoBehaviour
{
	SpriteRenderer sprite;
	Rigidbody2D body;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Mathf.Approximately(body.velocity.x, 0))
			sprite.flipX = body.velocity.x < 0;
	}
}
