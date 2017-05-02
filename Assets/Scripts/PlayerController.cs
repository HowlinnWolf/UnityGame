using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	public float speed = 0;
	float realSpeed;
	
	// Update is called once per frame
	void FixedUpdate () { 
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (Input.touchCount > 0) {
				// The screen has been touched so store the touch
				Touch touch = Input.GetTouch (0);
				realSpeed = 1.2f + speed / 10;
			
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
			
			// Mouse Controls for Debugging
			else if (Input.GetMouseButton (0)) {
				realSpeed = 1.2f + speed / 10;
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