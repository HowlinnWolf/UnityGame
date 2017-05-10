using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsDestroyer : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Projectile") {
			Destroy (other.gameObject);
		}
	}
}
