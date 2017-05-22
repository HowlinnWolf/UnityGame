using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
	light, medium, heavy
}

public class EnemyController : MonoBehaviour {

	public enemyType enemyType;
	public float bulletsFireRate = 0;
	public float lasersFireRate = 0;
	public int gunsRotation = 0;

	public AudioSource shotSound;
	public AudioSource laserSound;

	private GameObject shot;     // For bullet guns
	private GameObject laser;	 // For laser guns
	private float wideGunsFireRate = 1.5f;  // Wide guns shoot once per 0.3 seconds (when fire rate level is 0)
	private float frontGunsFireRate = 2f; // Front guns shoot once per 1.5 seconds (when fire rate level is 0)
	private float wideNextFire;
	private float frontNextFire;

	private Transform gunSpawn;   // Middle gun shot spawn point
	private Transform lGunSpawn;  // Left wide gun shot spawn point
	private Transform rGunSpawn;  // Right wide gun shot spawn point

	void Start() {
		//shotSound = AudioSource.FindObjectOfType<AudioSource> ();
		//laserSound = AudioSource.FindObjectOfType<AudioSource> ();
		shot = Resources.Load<GameObject> ("b_enemyBullet");
		laser = Resources.Load<GameObject> ("b_enemyLaser");
		// Picking shot spawn points:
		if (enemyType == enemyType.light) {
			gunSpawn = transform.Find ("Gun");
		} else if (enemyType == enemyType.medium) {
			lGunSpawn = transform.Find ("Gun_L");
			rGunSpawn = transform.Find ("Gun_R");
		} else if (enemyType == enemyType.heavy) {
			gunSpawn = transform.Find ("Gun");
			lGunSpawn = transform.Find ("Gun_L");
			rGunSpawn = transform.Find ("Gun_R");
		}
	}

	void Update() {
		if (enemyType == enemyType.light) {
			if (Time.time > wideNextFire) {
				wideNextFire = Time.time + wideGunsFireRate - bulletsFireRate / 40;
				Instantiate (shot, gunSpawn.position, gunSpawn.rotation);
				shotSound.Play ();
			} 
		} else if (enemyType == enemyType.medium) {
			if (Time.time > frontNextFire) {
				frontNextFire = Time.time + frontGunsFireRate - lasersFireRate / 20;
				Instantiate (laser, lGunSpawn.position, lGunSpawn.rotation);
				Instantiate (laser, rGunSpawn.position, rGunSpawn.rotation);
				laserSound.Play ();
				
			}
		} else if (enemyType == enemyType.heavy) {
			if (Time.time > wideNextFire) {
				wideNextFire = Time.time + wideGunsFireRate - lasersFireRate / 20;
				Instantiate (laser, gunSpawn.position, gunSpawn.rotation);
				shotSound.Play ();
			} 
			if (Time.time > frontNextFire) {
				frontNextFire = Time.time + frontGunsFireRate - bulletsFireRate / 40;
				Instantiate (shot, lGunSpawn.position, Quaternion.Euler(new Vector3(0, 0, gunsRotation)));
				Instantiate (shot, rGunSpawn.position, Quaternion.Euler(new Vector3(0, 0, -gunsRotation)));
				laserSound.Play ();
			}
		}
	}
}
