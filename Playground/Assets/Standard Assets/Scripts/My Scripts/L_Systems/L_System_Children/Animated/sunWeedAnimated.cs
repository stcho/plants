﻿using UnityEngine;
using System.Collections;

public class sunWeedAnimated : L_System_Animated {

	// Use this for initialization
	void Start () 
	{
		//Initialize axiom
		returnList = "F";

		//Add productions
		productions = new ArrayList ();
		productions.Add ("[*F]F%F[F]");

		angleAnimateSpeed = 0.1f;
		edgeAnimateSpeed = 0.001f;

		initializeAnimation ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Animate ();
	}
}
