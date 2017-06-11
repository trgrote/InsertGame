using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call on this script to spawn a whale at any of the locations specified
public class WhaleSpawner : MonoBehaviour
{
	[SerializeField] GameObject whale;
	[SerializeField] Transform[] spawnPoints;

	// Spawn a whale and return it to the caller
	public GameObject SpawnWhale()
	{
		var transform = spawnPoints[Random.Range(0, spawnPoints.Length)];

		return Instantiate(whale, transform.position, transform.rotation);
	}
}
