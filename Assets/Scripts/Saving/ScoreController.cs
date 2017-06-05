using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public int highscore;
	public int score;
	public Text highscoreLabel;
	public Text scoreLabel;
	// Use this for initialization
	void Start () {
		scoreLabel.text = score.ToString ();
		if (PlayerPrefs.HasKey ("highscore")) {
			highscore = PlayerPrefs.GetInt ("highscore");
			highscoreLabel.text = highscore.ToString ();
		} else
			highscoreLabel.text = highscore.ToString ();
	}

	public void AddToScore(int points){
		score += points;
		scoreLabel.text = score.ToString ();
		if (score > highscore) {
			highscore = score;
		}
	}

	public void SaveHighscore(){
		PlayerPrefs.SetInt ("highscore", highscore);
		PlayerPrefs.Save ();
	}
}
