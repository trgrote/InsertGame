using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scale the size of the camera based off how fast a target is moving
// The faster its going, the bigger the camera becomes
[RequireComponent(typeof(Camera))]
public class CameraSizeScaling : MonoBehaviour
{
	Camera cam;

	[SerializeField] public Rigidbody2D target;

	[Header("Config")]
	[SerializeField] float minSize = 10;
	[SerializeField] float maxSize = 30;
	[SerializeField] float velocityToSize = 5;
	[SerializeField] float trackSpeed = 5;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (target)
		{
			cam.orthographicSize = Mathf.Lerp(
				cam.orthographicSize,
				Mathf.Clamp(target.velocity.magnitude * velocityToSize, minSize, maxSize),
				Time.deltaTime * trackSpeed
			);
		}
	}
}
