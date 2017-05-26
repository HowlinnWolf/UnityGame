using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
	light, medium, heavy
}

public class EnemyController : MonoBehaviour {

	public enemyType enemyType;
	[Range(0, 10)]
	public float bulletsFireRate = 0;
	[Range(0, 10)]
	public float lasersFireRate = 0;
	public int gunsRotation = 0;

	public AudioSource shotSound;
	public AudioSource laserSound;

	private GameObject shot;     // For bullet guns
	private GameObject laser;	 // For laser guns
	private float wideGunsFireRate = 1f;  // Wide guns shoot once per 1 seconds (when fire rate level is 0)
	private float frontGunsFireRate = 2f; // Front guns shoot once per 1.5 seconds (when fire rate level is 0)

	private int enemyHealth;

	private Transform gunSpawn;   // Middle gun shot spawn point
	private Transform lGunSpawn;  // Left wide gun shot spawn point
	private Transform rGunSpawn;  // Right wide gun shot spawn point
	private Transform lFGunSpawn; // Left front gun
	private Transform rFGunSpawn; // Right front gun

	void Start() {
		EnemyManager enemyManager = this.GetComponent<EnemyManager>();
		enemyHealth = enemyManager.health;
		shot = Resources.Load<GameObject> ("b_enemyBullet");
		laser = Resources.Load<GameObject> ("b_enemyLaser");
		// Picking shot spawn points:
		if (enemyType == enemyType.light) {
			gunSpawn = transform.Find ("Gun");
			StartCoroutine (LightShoot (3, 5));
		} else if (enemyType == enemyType.medium) {
			lGunSpawn = transform.Find ("Gun_L");
			rGunSpawn = transform.Find ("Gun_R");
			StartCoroutine (MediumShoot (1, 0));
		} else if (enemyType == enemyType.heavy) {
			lGunSpawn = transform.Find ("Gun_L");
			rGunSpawn = transform.Find ("Gun_R");
			lFGunSpawn = transform.Find ("FGun_L");
			rFGunSpawn = transform.Find ("FGun_R");
			StartCoroutine (HeavyFrontShoot (1, 0));
			StartCoroutine (HeavyWideShoot (2, 2));
		}
	}

	IEnumerator LightShoot(int shotsInShot, int shotsWait) {
		while (enemyHealth > 0) {
			for (int i = 0; i < shotsInShot; i++) {
				Instantiate (shot, gunSpawn.position, gunSpawn.rotation);
				shotSound.Play ();
				yield return new WaitForSeconds (wideGunsFireRate - bulletsFireRate/10);
			}
			yield return new WaitForSeconds (shotsWait);
		}
	}

	IEnumerator MediumShoot(int shotsInShot, int shotsWait) {
		while (enemyHealth > 0) {
			for (int i = 0; i < shotsInShot; i++) {
				Instantiate (laser, lGunSpawn.position, lGunSpawn.rotation);
				Instantiate (laser, rGunSpawn.position, rGunSpawn.rotation);
				laserSound.Play ();
				yield return new WaitForSeconds (frontGunsFireRate - lasersFireRate/10);
			}
			yield return new WaitForSeconds (shotsWait);
		}
	}

	IEnumerator HeavyWideShoot(int shotsInShot, int shotsWait) {
		while (enemyHealth > 0) {
			for (int i = 0; i < shotsInShot; i++) {
				Instantiate (shot, lGunSpawn.position, Quaternion.Euler(new Vector3(0, 0, gunsRotation)));
				Instantiate (shot, rGunSpawn.position, Quaternion.Euler(new Vector3(0, 0, -gunsRotation)));
				laserSound.Play ();
				yield return new WaitForSeconds (wideGunsFireRate - bulletsFireRate/10);
			}
			yield return new WaitForSeconds (shotsWait);
		}
	}

	IEnumerator HeavyFrontShoot(int shotsInShot, int shotsWait) {
		while (enemyHealth > 0) {
			for (int i = 0; i < shotsInShot; i++) {
				Instantiate (laser, lFGunSpawn.position, lFGunSpawn.rotation);
				Instantiate (laser, rFGunSpawn.position, rFGunSpawn.rotation);
				laserSound.Play ();
				yield return new WaitForSeconds (frontGunsFireRate - lasersFireRate/10);
			}
			yield return new WaitForSeconds (shotsWait);
		}
	}
}
