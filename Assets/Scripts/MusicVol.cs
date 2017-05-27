using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVol : MonoBehaviour {

	public float startVolume = 0.8f;
	// Use this for initialization
	void Start () {
		AudioListener.volume = startVolume;
	}
}
