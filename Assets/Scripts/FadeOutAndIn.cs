using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutAndIn : MonoBehaviour
{
	Image image;

	void Start()
	{
		image = GetComponent<Image>();
	}

	// Time for one of the fade cycles so total effect time = fadeOutTime * 2
	IEnumerator FadeInAndOut(float fadeOutTime)
	{
		float elapsedTime = 0f;
		Color color = image.color;

		while (elapsedTime < fadeOutTime)
		{
			color.a = elapsedTime / fadeOutTime;
			image.color = color;
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		image.color = color = Color.black;
		yield return null;

		elapsedTime = 0f;
		while (elapsedTime < fadeOutTime)
		{
			color.a = 1 - elapsedTime / fadeOutTime;
			image.color = color;
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// set to completely invisible
		color.a = 0f;
		image.color = color;

		yield return null;
	}

	public void DoFade(float fadeOutTime)
	{
		StartCoroutine(FadeInAndOut(fadeOutTime));
	}
}
