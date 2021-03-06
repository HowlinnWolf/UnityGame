﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	bool paused = false;
	public GameObject pauseMenu;
	public GameObject pauseButton;
	public GameObject fadingImage;
	private FadeInOut fadeInOut;

	private float curVol;
	private float curTimeScale;

	void Start () {
		AudioListener.volume = 0.4f;
		if (fadeInOut) {
			fadeInOut = fadingImage.GetComponent<FadeInOut> ();
		}
	}

	public void pauseGame(){
		if (!paused) {
			if (fadeInOut) {
				fadeInOut.Fade (0.7f, 0.1f);
			}
			curVol = AudioListener.volume;
			curTimeScale = Time.timeScale;
			pauseButton.SetActive (false);
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
			AudioListener.volume = Mathf.Lerp (AudioListener.volume, 0.1f, 0.2f);
			paused = true;
		} else {
			if (fadeInOut) {
				fadeInOut.Fade (0.3f, 0.1f);
			}
			pauseButton.SetActive (true);
			pauseMenu.SetActive (false);
			Time.timeScale = curTimeScale;
			AudioListener.volume = Mathf.Lerp (AudioListener.volume, curVol, 0.2f);
			paused = false;
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {	// Pause and Unpause with "back" button
			pauseGame();
		}
	}

	public void PlayLevel(string scene){
		Time.timeScale = 1f;
		AudioListener.volume = Mathf.Lerp (AudioListener.volume, 0.8f, 0.2f);
		paused = false;
		SceneManager.LoadScene(scene);
	}

	public void ReplayLevel(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (scene.name);
	}

	public void QuitApp(){ 
		Application.Quit(); 
	}

}
