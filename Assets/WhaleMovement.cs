using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhaleState
{
	Sitting,
	Moving,
	Jump1,
	Jump2,
	Death,
	Done
}

public class WhaleMovement : MonoBehaviour {
	private WhaleState state;
	private Rigidbody2D body;
	private float timeAllotted;
	[SerializeField] private Sprite FirstJump;
	[SerializeField] private Sprite SecondJump;
	[SerializeField] private Sprite DeadChunks;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		state = WhaleState.Sitting;
		timeAllotted = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			state = WhaleState.Moving;
		}
		if (Input.GetKeyUp (KeyCode.Space)) 
		{
			state = WhaleState.Jump1;
		}
		switch (state) 
		{
		case WhaleState.Moving:
			body.AddForce (new Vector2 (0, 5));
			break;
		case WhaleState.Jump1:
			GetComponent<SpriteRenderer> ().sprite = FirstJump;
			GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
			timeAllotted += Time.deltaTime;
			if (timeAllotted >= 0.6) { state = WhaleState.Jump2; }
			break;
		case WhaleState.Jump2:
			GetComponent<SpriteRenderer> ().sprite = SecondJump;
			body.velocity = new Vector2 (0, 0);
			timeAllotted += Time.deltaTime;
			if (timeAllotted >= 3.0) {
				state = WhaleState.Death;
			}
			break;
		case WhaleState.Death:
			GetComponent<SpriteRenderer> ().sprite = DeadChunks;
			GetComponent<Explodable> ().explode ();
			var circlePosition = new Vector2 (transform.position.x, transform.position.y);
			RaycastHit2D[] people = Physics2D.CircleCastAll (circlePosition, 2.5f, Vector2.up);
			foreach (RaycastHit2D person in people) 
			{
				var personPosition = person.point;
				var power = personPosition - circlePosition;
				power.Normalize ();
				power *= Random.Range (0, 10);
				person.rigidbody.AddForce (power, ForceMode2D.Impulse);
			}
			Debug.Log (people.Length);
			//body.AddForce (new Vector2 (100, 100), ForceMode2D.Impulse);
			state = WhaleState.Done;
			break;
		}
	}
}
