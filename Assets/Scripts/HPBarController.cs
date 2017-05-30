using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {

	public bool IsPlayers;
	public RectTransform healthTransform;
	private float cachedY;
	public float minXValue;
	public float maxXValue;
	public int curHealth;
	private int maxHealth;
	public Text labelText;

	PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("Player");
		playerManager = player.GetComponent<PlayerManager> ();
		maxHealth = playerManager.health;

		cachedY = healthTransform.position.y;
		maxXValue = healthTransform.position.x;
		if (IsPlayers)
			minXValue = healthTransform.position.x - healthTransform.rect.width;
		else
			minXValue = healthTransform.position.x + healthTransform.rect.width;
		
		curHealth = maxHealth;
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	public void MoveHealthBar(){
		float curX =  MapValues (curHealth, 0, maxHealth, minXValue, maxXValue);
		healthTransform.position = new Vector3 (curX, cachedY);
	}
}
