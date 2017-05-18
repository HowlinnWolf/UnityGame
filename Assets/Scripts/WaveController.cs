using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WaveController {
	public GameObject[] hazards;
	public int enemiesInWave;	// Amount of enemies is wave
	public float spawnWait;
	public float waveStartWait;
	public float waveEndWait;
}