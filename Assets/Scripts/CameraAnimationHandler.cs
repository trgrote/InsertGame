using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The point of this script is to start the pan animation and then when it ends,
// turn on the whale tracking crap
[RequireComponent(typeof(Animator), typeof(CameraFollow), typeof(CameraSizeScaling))]
public class CameraAnimationHandler : MonoBehaviour, IEventHandler
{
	private WhaleSpawner spawner;

	void Start()
	{
		spawner = FindObjectOfType<WhaleSpawner>();

		EventBroadcaster.registerHandler<WhaleHasDiedEvent>(this);
	}

	void OnDisable()
	{
		EventBroadcaster.unregsterHandler(this);
	}

	public void handleEvent( IGameEvent evt )
	{
		if (evt is WhaleHasDiedEvent)
		{
			StartCoroutine(WaitAndSee());
		}
	}

	IEnumerator WaitAndSee()
	{
		yield return new WaitForSeconds(3);
		OnWhaleDeath();
		yield return null;
	}

	// When the pan animation ends, then turn on these bags of buttholes
	void OnPanEnd()
	{
		if (spawner == null)
		{
			Debug.Log("Can't Find spawner in scene");
			return;
		}

		var whale = spawner.SpawnWhale();

		var follow = GetComponent<CameraFollow>();
		var size = GetComponent<CameraSizeScaling>();

		follow.target = whale.transform;
		size.target = whale.GetComponent<Rigidbody2D>();

		GetComponent<Animator>().enabled = false;
		follow.enabled = true;
		size.enabled = true;
	}

	// When the whale dies, turn on animator again and play the pan animation
	void OnWhaleDeath()
	{
		GetComponent<CameraFollow>().enabled = false;
		GetComponent<CameraSizeScaling>().enabled = false;

		// Start animation again
		var anim = GetComponent<Animator>();
		anim.enabled = true;
		anim.Play("CameraPan", -1, 0f);
	}
}
