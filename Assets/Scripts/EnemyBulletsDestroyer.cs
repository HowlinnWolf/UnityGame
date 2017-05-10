using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletsDestroyer : MonoBehaviour {
	
	void Update () {
		Destroy (gameObject, 10f);
	}
}
