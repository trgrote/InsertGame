﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhaleState
{
	Sitting,
	Moving,
	Jump1,
	Jump2,
	Bloat,
	Death,
	Done
}

public class WhaleMovement : MonoBehaviour {
	private WhaleState state;
	private Rigidbody2D body;
	private float timeAllotted;
	private AudioSource audio;
	private float currentAngle;
	private float spriteAngle;
	private bool isPastCoast;
	[SerializeField] private Sprite Swimming;
	[SerializeField] private Sprite FirstJump;
	[SerializeField] private Sprite SecondJump;
	[SerializeField] private Sprite Bloated;
	[SerializeField] private Sprite DeadChunks;
	[SerializeField] private AudioClip SwimNoise;
	[SerializeField] private AudioClip JumpNoise;
	[SerializeField] private AudioClip LandNoise;
	[SerializeField] private AudioClip SkidNoise;
	[SerializeField] private AudioClip FartNoise;
	[SerializeField] private AudioClip ExplosionNoise;
	[SerializeField] public GameObject CraterPrefab;

	// Use this for initialization
	void Start () {
		audio = GameObject.Find ("WhaleNoises").GetComponent<AudioSource> ();
		GetComponent<TrailRenderer> ().enabled = false;
		body = GetComponent<Rigidbody2D> ();
		state = WhaleState.Sitting;
		timeAllotted = 0;
		currentAngle = 0;
		isPastCoast = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && state == WhaleState.Sitting) 
		{
			state = WhaleState.Moving;
			audio.clip = SwimNoise;
			audio.Play ();
		}
		if (Input.GetKeyUp (KeyCode.Space) && state == WhaleState.Moving) 
		{
			state = WhaleState.Jump1;
			audio.clip = JumpNoise;
			audio.Play ();
		}
		if (isPastCoast && (state == WhaleState.Moving || state == WhaleState.Sitting)) {
			GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
			audio.clip = SkidNoise;
			audio.Play ();
			state = WhaleState.Jump2;
		}
		switch (state) 
		{
		case WhaleState.Moving:
			if (Input.GetKey (KeyCode.LeftArrow)) {
				currentAngle -= Time.deltaTime * 4;
				spriteAngle += Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				currentAngle += Time.deltaTime * 4;
				spriteAngle -= Time.deltaTime;
			}
			transform.Rotate (Vector3.forward * spriteAngle);
			body.AddForce (new Vector2 (currentAngle, 5));

			break;
		case WhaleState.Jump1:
			GetComponent<SpriteRenderer> ().sprite = FirstJump;
			GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
			timeAllotted += Time.deltaTime;
			if (timeAllotted >= 1) 
			{
				audio.clip = SkidNoise;
				audio.Play ();
				if (isPastCoast) {
					state = WhaleState.Jump2; 
				} else {
					timeAllotted = 0;
					GetComponent<SpriteRenderer> ().sprite = Swimming;
					GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
					state = WhaleState.Sitting;
				}
			}
			break;
		case WhaleState.Jump2:
			GetComponent<SpriteRenderer> ().sprite = SecondJump;
			GetComponent<TrailRenderer> ().enabled = true;
			body.drag = 3.0f;
			audio.volume -= Time.deltaTime;
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				state = WhaleState.Bloat;
				audio.volume = 1.0f;
				audio.clip = FartNoise;
				audio.Play ();
			}
			break;
		case WhaleState.Bloat:
			GetComponent<SpriteRenderer> ().sprite = Bloated;
			timeAllotted += Time.deltaTime;
			if (timeAllotted >= 3.0) {
				audio.Stop ();
				audio.clip = ExplosionNoise;
				audio.Play ();
				state = WhaleState.Death;
			}
			break;
		case WhaleState.Death:
			GetComponent<SpriteRenderer> ().sprite = DeadChunks;
			GetComponent<Explodable> ().explode ();
			Instantiate (CraterPrefab, transform.position, Quaternion.identity);
			var circlePosition = new Vector2 (transform.position.x, transform.position.y);
			RaycastHit2D[] people = Physics2D.CircleCastAll (circlePosition, 5.0f, Vector2.up);
			foreach (RaycastHit2D person in people) 
			{
				var personPosition = person.point;
				var power = personPosition - circlePosition;
				power.Normalize ();
				power *= Random.Range (0, 10);
				if (person.rigidbody != null) 
				{
					person.rigidbody.AddForce (power, ForceMode2D.Impulse);
				}
			}
			//body.AddForce (new Vector2 (100, 100), ForceMode2D.Impulse);
			state = WhaleState.Done;
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log ("HIT THE COAST LINE.");
		if (coll.gameObject.name == "WhaleCoastBreak") 
		{
			isPastCoast = true;
		}
	}
}
