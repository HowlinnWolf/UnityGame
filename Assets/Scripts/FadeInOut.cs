using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

	public Image image;
	public float wait = 0.5f;
	public float fadeDuration = 0.25f;
	public float endAlpha = 1f;
	public bool IsFade = false;

	IEnumerator Wait() {
		yield return new WaitForSeconds (wait);
	}

	public void Fade(float Alpha, float duration) {
		image.CrossFadeAlpha(Alpha, duration, true);
	}
		
	void Start() {
		StartCoroutine (Wait ());
		if (IsFade)
			Fade (endAlpha, fadeDuration);
	}
}
