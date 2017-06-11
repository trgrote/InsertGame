using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enable when whale spawns and disable when user hits space bar and 2 seconds
// have past
[RequireComponent(typeof(Image))]
public class TutorialOverlay : MonoBehaviour, IEventHandler
{
	Image image;

	// Use this for initialization
	void Start ()
	{
		image = GetComponent<Image>();

		EventBroadcaster.registerHandler<WhaleSpawned>(this);
		EventBroadcaster.registerHandler<WhaleMovedFirst>(this);
	}

	void OnDisable()
	{
		EventBroadcaster.unregsterHandler(this);
	}

	IEnumerator WaitAndDisable()
	{
		yield return new WaitForSeconds(2f);
		image.enabled = false;
	}

	public void handleEvent( IGameEvent evt )
	{
		if (evt is WhaleSpawned)
		{
			image.enabled = true;
		}

		if (evt is WhaleMovedFirst)
		{
			StartCoroutine(WaitAndDisable());
		}
	}
}
