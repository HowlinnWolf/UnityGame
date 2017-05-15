using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEnemyDestroyer : MonoBehaviour {
	
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
		}
	}
}
