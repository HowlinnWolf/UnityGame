using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {

	public bool IsEndless = false;
	public Transform spawnLine;
	public float range = 265f; // 270 - max
	public WaveController[] waves;
	private int playerHealth;
	private PlayerManager playerManager;

	void Start() {
		GameObject Player = GameObject.Find ("Player");
		playerManager = Player.GetComponent<PlayerManager>();
		playerHealth = playerManager.health;
		StartCoroutine (GameBody ());
	}

	IEnumerator GameBody(){
		for (int k = 0; k < waves.Length; k++){
			yield return new WaitForSeconds (waves[k].waveStartWait);	// Wait in the level beginning
			if (IsEndless) {
				while (playerHealth > 0) {	// "Endless" loop
					for (k = 0; k < waves.Length; k++){
						for (int i = 0; i < waves[k].enemiesInWave; i++) {
							Vector3 spawnPosition = new Vector3 (Random.Range (-range, range), 
								spawnLine.position.y, spawnLine.position.z);
							Instantiate (waves[k].hazards[Random.Range(0, waves[k].hazards.Length)], 
								spawnPosition, spawnLine.rotation);
							playerHealth = playerManager.health;				  // Check if player is still alive
							if (playerHealth < 1) break;						  // And exit if he's dead
							yield return new WaitForSeconds (waves[k].spawnWait); // Wait between spawns
						}
						yield return new WaitForSeconds (waves[k].waveEndWait);	// Wait after wave
					}
				}
			} else {
				for (int i = 0; i < waves[k].enemiesInWave; i++) {
					Vector3 spawnPosition = new Vector3 (Random.Range (-range, range), 
						spawnLine.position.y, spawnLine.position.z);
					Instantiate (waves[k].hazards[Random.Range(0, waves[k].hazards.Length)], 
						spawnPosition, spawnLine.rotation);
					yield return new WaitForSeconds (waves[k].spawnWait); // Wait between spawns
				}
				yield return new WaitForSeconds (waves[k].waveEndWait);	// Wait after wave
			}
		}
	}
}
