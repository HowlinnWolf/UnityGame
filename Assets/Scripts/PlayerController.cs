using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic; // 4 IsPointerUIObject

public class PlayerController : MonoBehaviour {

	// STATS:
	public float speed = 0;		// Speed level (0-10, 10+)
	private float realSpeed;
	public GameObject pauseButton;
	public GameObject fadingImage;

	private PlayerManager playerManager;
	private FadeInOut fadeInOut;
	private bool firstTouch;

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	void IsSlowMo (bool yeah){
		if (yeah){
			Time.timeScale = 0.1f;
			Time.fixedDeltaTime = 0.02f * 0.1f;
			AudioListener.volume = Mathf.Lerp(AudioListener.volume, 0.4f, 0.2f);
			pauseButton.SetActive (true);
		}
		else{
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f;
			AudioListener.volume = Mathf.Lerp(AudioListener.volume, 0.8f, 0.2f);
			pauseButton.SetActive (false);
		}
	}


	void Start(){
		firstTouch = true;
		IsSlowMo(false);
		fadeInOut = fadingImage.GetComponent<FadeInOut> ();
		playerManager = this.GetComponent<PlayerManager> ();
		playerManager.IsShooting = false;
	}

	void FixedUpdate () { 
		if (!IsPointerOverUIObject()) {
			// Touch controls:
			if (Input.touchCount > 0) {
				if (firstTouch) {
					IsSlowMo(false);
					fadeInOut.Fade (0f, 0.5f);
					playerManager.IsShooting = true;
					firstTouch = false;
				}

				// The screen has been touched so store the touch
				Touch touch = Input.GetTouch (0);
				realSpeed = 2f + speed / 10;
			
				if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) { 
					// If the finger is on the screen, move the object smoothly to the touch position
					Vector3 touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 21));                
					float mag = (transform.position - touchPosition).magnitude;
					transform.position =
						Vector3.MoveTowards (transform.position, touchPosition,
						mag > 50f
							? realSpeed * Time.deltaTime * 200
							: Mathf.Lerp (realSpeed * 0.1f, 200 * realSpeed, mag / 100f)
						* Time.deltaTime);
				}
			}
			
			// Mouse Controls for easy Debugging:
			else if (Input.GetMouseButton (0)) {
				if (firstTouch) {
					IsSlowMo (false);
					fadeInOut.Fade (0f, 0.5f);
					playerManager.IsShooting = true;
					firstTouch = false;
				}

				realSpeed = 2f + speed / 10;
				Vector3 clickPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 21));
				float mag = (transform.position - clickPosition).magnitude;
				transform.position =
					Vector3.MoveTowards (transform.position, clickPosition,
					mag > 50f
						? realSpeed * Time.deltaTime * 200
						: Mathf.Lerp (realSpeed * 0.1f, 200 * realSpeed, mag / 100f)
					* Time.deltaTime);
			
			// Slow-motion when not pressing anyhing
			} else {
				if (!firstTouch) {
					IsSlowMo (true);
					fadeInOut.Fade (0.6f, 0.5f);
					firstTouch = true;
				}
			}
			
		}
	}
}