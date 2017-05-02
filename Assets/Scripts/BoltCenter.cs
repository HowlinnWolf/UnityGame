using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCenter : MonoBehaviour {

	Rigidbody2D myRigidbody;
	public float boltCenterSpeed;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myRigidbody.AddRelativeForce (Vector2.up * boltCenterSpeed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
