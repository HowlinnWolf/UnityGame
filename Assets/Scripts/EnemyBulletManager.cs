using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour {

	int damage = 0;
	PlayerManager playerManager;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			if (other.GetComponent<PlayerManager>() != null){
				if (gameObject.tag == "EnemyBullet") {			// If projectile is Bullet
					damage = 1;
				} else if (gameObject.tag == "EnemyLaser") {	// If projectile is Laser
					damage = 3;
				} else {
					return;
				}
				other.GetComponent<PlayerManager> ().TakeDamage (damage);
				Destroy (gameObject);
			}
		}
	}
}
