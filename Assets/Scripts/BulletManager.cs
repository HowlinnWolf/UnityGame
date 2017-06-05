using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	private int mainDamage = 0;
	private int takenDamage;
	private PlayerManager playerManager;

	void Start () {
		GameObject Player = GameObject.Find ("Player");
		playerManager = Player.GetComponent<PlayerManager>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			mainDamage = playerManager.damage;
			if (other.GetComponent<EnemyManager>() != null){
				if (gameObject.tag == "PlayerBullet") {			// If projectile is Bullet
					takenDamage = mainDamage + 1;
				} else if (gameObject.tag == "PlayerLaser") {	// If projectile is Laser
					takenDamage = mainDamage + 3;
				} else {
					return;
				}
				other.GetComponent<EnemyManager> ().TakeDamage (takenDamage);
				Destroy (gameObject);
			}
		}
	}
}
