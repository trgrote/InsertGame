using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This heckin' script will be the basic AI and movement for the silly people
public class PeopleMovement : MonoBehaviour
{
	const float walkSpeed = 1f;
	const float walkTime = 0.5f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(IdleWalkCycle());
	}

	// Walk a bit in a random direction
	IEnumerator WalkForABit()
	{
		// Choose a random direction
		var direction = Random.insideUnitCircle;

		var rigid = GetComponent<Rigidbody2D>();

		rigid.velocity = direction * walkSpeed;
		yield return new WaitForSeconds(walkTime);
		rigid.velocity = Vector2.zero;

		yield return null;
	}

	// Alternate between standing still and moving around a bit
	IEnumerator IdleWalkCycle()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(0.5f, 20));  // idle time
			yield return WalkForABit();
		}
	}
}
