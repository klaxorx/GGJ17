﻿using UnityEngine;
using System.Collections;

public class Trap : Item {

    public bool used { get; private set; }
	// Use this for initialization
	void Start () {

        investigationTime = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
