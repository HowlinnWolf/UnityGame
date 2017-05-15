using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBulletsDestroyer : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Projectile" ||
			other.gameObject.tag == "PlayerBullet" ||
			other.gameObject.tag == "PlayerLaser") {
			Destroy (other.gameObject);
		}
	}
}
