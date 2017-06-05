using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {

	public int curHealth;
	public int maxHealth;
	private float hpBar;
	public Text labelText;

	PlayerManager playerManager;
	Image image;

	// Use this for initialization
	void Start () {

		GameObject player = GameObject.Find ("Player");
		playerManager = player.GetComponent<PlayerManager> ();
		image = this.GetComponent<Image> ();
		maxHealth = playerManager.health;
		curHealth = maxHealth;
		MoveHealthBar ();
	}

	public void MoveHealthBar(){
		hpBar = (float)curHealth / maxHealth;
		image.fillAmount = hpBar;
	}
}
