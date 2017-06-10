using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomDeathScream : MonoBehaviour
{
	// Chance to play any sound
	[SerializeField, Range(0,1)] float chanceToPlay = 0.2f;
	[SerializeField] AudioClip[] clips;

	// Use this for initialization
	void Start ()
	{
		if (Random.value < chanceToPlay)
		{
			var audio = GetComponent<AudioSource>();
			audio.clip = clips[
				Random.Range(0, clips.Length)
			];
			audio.Play();
		}
	}
}
