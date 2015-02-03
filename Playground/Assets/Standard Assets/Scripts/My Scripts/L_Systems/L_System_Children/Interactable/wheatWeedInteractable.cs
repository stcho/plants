using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class wheatWeedInteractable : Parametric_L_System {

	private float branchDelay = 1.0f;
	
	private float segmentBirth = 0;
	private float segmentMaturity = 1.0f;

	// Use this for initialization
	void Start () 
	{
		animateSpeed = 2.0f;
		growthRate = 0.075f;

		branchAngle = 45.0f;

		returnList = new List<Module> ();
		productions = new Dictionary<char, List<Module>> ();

		returnList.Add (new Module ('F', segmentBirth, segmentMaturity, initialSegmentLength));
		returnList.Add (new Module ('a', 0, 1, 0));

		productions.Add ('a', new List<Module>());
		productions['a'].Add (new Module ('F', segmentBirth, segmentMaturity, initialSegmentLength));
		productions['a'].Add (new Module ('[', 0, -1, -1));
		productions['a'].Add (new Module ('%', 0, -1, branchAngle));
		productions['a'].Add (new Module ('b', 0, branchDelay, -1));
		productions['a'].Add (new Module (']', 0, -1, -1));
		productions['a'].Add (new Module ('[', 0, -1, -1));
		productions['a'].Add (new Module ('*', 0, -1, branchAngle));
		productions['a'].Add (new Module ('b', 0, branchDelay, -1));
		productions['a'].Add (new Module (']', 0, -1, -1));
		productions['a'].Add (new Module ('F', segmentBirth, segmentMaturity, initialSegmentLength));
		productions['a'].Add (new Module ('a', 0, segmentMaturity, -1));

		productions.Add ('b', new List<Module>());
		productions['b'].Add (new Module('F', segmentBirth, segmentMaturity, initialSegmentLength));
		productions['b'].Add (new Module ('a', 0, segmentMaturity, -1));
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override float growthFunction(char sym, float gp, float time)
	{
		switch(sym)
		{
			case 'F':
				return growthRate*(gp)*(1 - (time/segmentMaturity));
			break;
			default:
				return 0;
			break;
		}
	}
	
}
