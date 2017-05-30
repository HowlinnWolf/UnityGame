using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	bool paused = false;
	//public GameObject darkTexture; // To cover gameplay while you're in the menu and/or not touching screen
	public GameObject pauseMenu;
	public GameObject pauseButton;

	private float curVol;
	private float curTimeScale;

	void Start () {
		AudioListener.volume = 0.4f;
	}

	public void pauseGame(){
		if (!paused) {
			curVol = AudioListener.volume;
			curTimeScale = Time.timeScale;
			pauseButton.SetActive (false);
			//darkTexture.SetActive (true);
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
			AudioListener.volume = Mathf.Lerp (AudioListener.volume, 0.2f, 0.2f);
			paused = true;
		} else {
			//darkTexture.SetActive (false);
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
		SceneManager.LoadScene(scene);
	}

	public void ReplayLevel(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (scene.name);
	}

	public void BackToMenu(){
		Time.timeScale = 1f;
		AudioListener.volume = Mathf.Lerp (AudioListener.volume, 0.8f, 0.2f);
		paused = false;
		PlayLevel("MainMenu");
	}

	public void QuitApp(){ 
		Application.Quit(); 
	}

}
