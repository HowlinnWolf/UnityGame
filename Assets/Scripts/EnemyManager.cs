using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public int health;
	public GameObject explosion;
	private ScoreController scoreController;
	private bool hasScore = false;

	public void TakeDamage (int damage) {
		health = health - damage;
		if (health <= 0) {
			Die ();
		}
	}

	public void Die () {
		if (GameObject.Find("ScoreField"))
			hasScore = true;
		if (hasScore) {
			GameObject scoreField = GameObject.Find ("ScoreField");
			scoreController = scoreField.GetComponent<ScoreController> ();

			if (this.gameObject.name.Contains ("enmy_light"))
				scoreController.AddToScore (1);
			else if (this.gameObject.name.Contains ("enmy_medium"))
				scoreController.AddToScore (5);
			else if (this.gameObject.name.Contains ("enmy_heavy"))
				scoreController.AddToScore (10);
		}

		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
