﻿using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

	// Use this for initialization
	void Start () {
        WaveManager.Instance.StartWave();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
