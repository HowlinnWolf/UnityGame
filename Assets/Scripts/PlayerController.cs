using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic; // 4 IsPointerUIObject

public class PlayerController : MonoBehaviour {

	// STATS:
	public float speed = 0;		// Speed level (0-10, 10+)
	public float fireRate = 0;  // Fire rate level (0-10, 10+)
	//public float armor = 0;   // TODO

	private GameObject wideShot;  // Projectile for wide guns
	private GameObject frontShot; // Projectile for frong guns
	private Transform lWideGunSpawn;  // Left wide gun shot spawn point
	private Transform rWideGunSpawn;  // Right wide gun shot spawn point
	private Transform lFrontGunSpawn; // Left front gun shot spawn point
	private Transform rFrontGunSpawn; // Right front gun shot spawn point

	private float wideGunsFireRate = 0.3f;  // Wide guns shoot once per 0.3 seconds (when fire rate level is 0)
	private float frontGunsFireRate = 1.5f; // Front guns shoot once per 1.5 seconds (when fire rate level is 0)

	private float wideNextFire;
	private float frontNextFire;
	private float realSpeed;

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	void Start () {
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
		// Shooting with fire rate:
		if (Time.time > wideNextFire)
		{
			wideNextFire = Time.time + wideGunsFireRate - fireRate/40;
			Instantiate(wideShot, lWideGunSpawn.position, lWideGunSpawn.rotation);
			Instantiate(wideShot, rWideGunSpawn.position, rWideGunSpawn.rotation);
		} 
		if (Time.time > frontNextFire)
		{
			frontNextFire = Time.time + frontGunsFireRate - fireRate/20;
			Instantiate(frontShot, lFrontGunSpawn.position, lFrontGunSpawn.rotation);
			Instantiate(frontShot, rFrontGunSpawn.position, rFrontGunSpawn.rotation);
		}
	}

	void FixedUpdate () { 
		//if (!EventSystem.current.IsPointerOverGameObject()) {
		if (!IsPointerOverUIObject()) {
			// Touch controls:
			if (Input.touchCount > 0) {
				// The screen has been touched so store the touch
				Touch touch = Input.GetTouch (0);
				realSpeed = 2f + speed / 10;
			
				if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) { 
					// If the finger is on the screen, move the object smoothly to the touch position
					Vector3 touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 1));                
					float mag = (transform.position - touchPosition).magnitude;
					transform.position =
						Vector3.MoveTowards(transform.position, touchPosition,
							mag > 50f
							? realSpeed * Time.deltaTime * 200
							: Mathf.Lerp(realSpeed * 0.1f, 200 * realSpeed, mag / 100f)
							* Time.deltaTime);
				}
			}
			
			// Mouse Controls for easy Debugging:
			else if (Input.GetMouseButton (0)) {
				realSpeed = 2f + speed / 10;
				Vector3 clickPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
				float mag = (transform.position - clickPosition).magnitude;
				transform.position =
					Vector3.MoveTowards(transform.position, clickPosition,
						mag > 50f
						? realSpeed * Time.deltaTime * 200
						: Mathf.Lerp(realSpeed * 0.1f, 200 * realSpeed, mag / 100f)
						* Time.deltaTime);
			}
		}
	}
}