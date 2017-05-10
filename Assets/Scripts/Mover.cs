using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	Rigidbody2D myRigidbody;
	public float speed;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myRigidbody.AddRelativeForce (Vector2.up * speed, ForceMode2D.Impulse);
	}
}
