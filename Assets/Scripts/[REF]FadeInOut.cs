using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

	public Image fadeOverlay;
	public float waitOnLoadEnd = 0.25f;
	public float fadeDuration = 0.25f;

	public void FadeIn() {
		fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
	}

	public void FadeOut() {
		fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
	}
}
