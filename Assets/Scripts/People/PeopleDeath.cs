using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On Death Replace yourself with a splatter
public class PeopleDeath : MonoBehaviour
{
	[SerializeField]
	GameObject deadPerson;

	void OnAliveChanged(bool alive)
	{
		Instantiate(
			deadPerson,
			transform.position,
			transform.rotation
		);

		// Do what all advertisers should do
		Destroy(gameObject);   // kill self
	}

#if DEBUG
	void Update()
	{
		// Test
		if (Input.GetKeyDown(KeyCode.K))
		{
			GetComponent<PeopleState>().Alive = false;
		}
	}
#endif
}
