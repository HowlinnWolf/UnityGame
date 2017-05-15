using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Transform spawnLine;
	public float range = 270f; // 270
	public int hazardCount;
	public float spawnWait;
	public float waveStartWait;
	public float waveEndtWait;

	private int playerHealth;

	void Start() {
		GameObject Player = GameObject.Find ("Player");
		PlayerManager playerManager = Player.GetComponent<PlayerManager>();
		playerHealth = playerManager.health;
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (waveStartWait);	// Wait in the level beginning
		while (playerHealth > 0){
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-range, range), spawnLine.position.y, spawnLine.position.z);
				Instantiate (hazard, spawnPosition, spawnLine.rotation);
				yield return new WaitForSeconds (spawnWait); // Wait between spawns
			}
			yield return new WaitForSeconds (waveEndtWait);	// Wait after wave
		}
	}
}
