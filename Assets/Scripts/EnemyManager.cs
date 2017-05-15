using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public int health;

	public void TakeDamage (int damage) {
		health = health - damage;
		if (health <= 0) {
			Die ();
		}
	}

	public void Die () {
		Destroy (gameObject);
	}
}
