using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	bool paused = false;
	public GameObject blurryBack;
	public GameObject pauseMenu;
	public GameObject pauseButton;

	private float curVol;
	private float curTimeScale;

	public void pauseGame(){
		if (!paused) {
			curVol = AudioListener.volume;
			curTimeScale = Time.timeScale;
			pauseButton.SetActive (false);
			blurryBack.SetActive (true);
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
			AudioListener.volume = 0.2f;
			paused = true;
		} else {
			blurryBack.SetActive (false);
			pauseMenu.SetActive (false);
			Time.timeScale = curTimeScale;
			AudioListener.volume = curVol;
			paused = false;
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {	// Pause and Unpause with "back" button
			pauseGame();
		}
	}

	public void Play(string scene){
		SceneManager.LoadScene(scene);
	}

	public void BackToMenu(){
		Time.timeScale = 1f;
		AudioListener.volume = 0.8f;
		paused = false;
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitApp(){ 
		Application.Quit(); 
	}

}
