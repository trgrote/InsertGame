using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera jumps to a target if one is specified
// This can be changed at run time by either enable/disabling the component or
// by just changing the transform target
public class CameraFollow : MonoBehaviour
{
	[SerializeField] public Transform target;
	// there's not really a unit for this guy
	[SerializeField] private float trackSpeed = 0.5f;
	private Vector3 offset = new Vector3(0, 5, 0);

	private float initialZ = -10;

	void Start()
	{
		initialZ = transform.position.z;
	}

	// Update is called once per frame
	void Update()
	{
		if (target)
		{
			var position = transform.position;

			position = Vector3.Lerp(
				position,
				target.position + offset,
				Time.deltaTime * trackSpeed
			);

			position.z = initialZ;   // z should be kept the same

			transform.position = position;
		}
	}
}
