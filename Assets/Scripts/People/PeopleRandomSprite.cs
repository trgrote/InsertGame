using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Choose Random Sprite for people
[RequireComponent(typeof(SpriteRenderer))]
public class PeopleRandomSprite : MonoBehaviour
{
	[SerializeField]
	Sprite[] possibleSprites;

	// Use this for initialization
	void Start ()
	{
		GetComponent<SpriteRenderer>().sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
	}
}
