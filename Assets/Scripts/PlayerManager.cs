using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public float fireRate = 0;  // Fire rate level (0-10)
	public int damage = 0;		// Projectile damage level
	public int armor = 0;   // TODO

	public int health = 500;
	public GameObject HPBar;
	public GameObject explosion;
	public GameObject messageWindow;
	public bool IsShooting = true;

	public AudioSource shot;
	public AudioSource laser;

	private GameObject wideShot;  // Projectile for wide guns
	private GameObject frontShot; // Projectile for frong guns
	private Transform lWideGunSpawn;  // Left wide gun shot spawn point
	private Transform rWideGunSpawn;  // Right wide gun shot spawn point
	private Transform lFrontGunSpawn; // Left front gun shot spawn point
	private Transform rFrontGunSpawn; // Right front gun shot spawn point

	private float wideGunsFireRate = 0.4f;  // Wide guns shoot once per 0.3 seconds (when fire rate level is 0)
	private float frontGunsFireRate = 1f; // Front guns shoot once per 1.5 seconds (when fire rate level is 0)

	private float wideNextFire;
	private float frontNextFire;

	private FadeInOut fadeInOut;
	public GameObject fadingImage;
	private HPBarController HP;

	void Start () {
		fadeInOut = fadingImage.GetComponent<FadeInOut> ();
		HP = HPBar.GetComponent<HPBarController> ();
		HP.curHealth = health;
		HP.labelText.text = health.ToString();
		wideShot = Resources.Load<GameObject> ("b_blueBullet");
		frontShot = Resources.Load<GameObject> ("b_blueLaser");
		// Looking for guns to attach spawn points
		lWideGunSpawn = transform.Find ("WideGuns/WGun_L");
		rWideGunSpawn = transform.Find ("WideGuns/WGun_R");
		lFrontGunSpawn = transform.Find ("FrontGuns/FGun_L");
		rFrontGunSpawn = transform.Find ("FrontGuns/FGun_R");
	}

	void Update ()
	{
		if (IsShooting) {
			// Shooting with fire rate:
			if (Time.time > wideNextFire) {
				wideNextFire = Time.time + wideGunsFireRate - fireRate / 40;
				Instantiate (wideShot, lWideGunSpawn.position, lWideGunSpawn.rotation);
				Instantiate (wideShot, rWideGunSpawn.position, rWideGunSpawn.rotation);
				shot.Play ();
			} 
			if (Time.time > frontNextFire) {
				frontNextFire = Time.time + frontGunsFireRate - fireRate / 20;
				Instantiate (frontShot, lFrontGunSpawn.position, lFrontGunSpawn.rotation);
				Instantiate (frontShot, rFrontGunSpawn.position, rFrontGunSpawn.rotation);
				laser.Play ();
			}
		}
	}

	public void TakeDamage (int damage) {
		health = health - damage;

		HP.curHealth = health;
		HP.labelText.text = health.ToString();
		HP.MoveHealthBar ();

		if (health <= 0) {
			Die ();
		}
	}

	public void Die () {
		Instantiate (explosion, transform.position, transform.rotation);
		messageWindow.SetActive (true);
		fadeInOut.Fade (0.8f, 1f);
		Destroy (gameObject);
	}

}
