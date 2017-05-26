using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public int health;
	public GameObject explosion;

	public void TakeDamage (int damage) {
		health = health - damage;
		if (health <= 0) {
			Die ();
		}
	}

	public void Die () {
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
