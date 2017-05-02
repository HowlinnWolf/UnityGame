using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	bool paused = false;

	public void pauseGame(){
		if (!paused) {
			Time.timeScale = 0f;
			AudioListener.pause = true;
			AudioListener.volume = 0f;
			paused = true;
		} else {
			Time.timeScale = 1f;
			AudioListener.pause = false;
			AudioListener.volume = 0.8f;
			paused = false;
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pauseGame();
		}
	}

	public void Play(){
		SceneManager.LoadScene("test");
	}

	public void BackToMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitApp(){ 
		Application.Quit(); 
	}

}
