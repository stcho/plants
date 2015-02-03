using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class monopodialInteractable : Parametric_L_System {

	private float initialBranchWidth = 1.0f;
	private float widthGrowth = 0.1f;
	
	private float branchAngleB = 45.0f;
	private float branchAngleC = 30.0f;
	private float divergenceAngle = 110.0f;

	// Use this for initialization
	void Start () 
	{
		returnList = new List<Module> ();
		productions = new Dictionary<char, List<Module>> ();

		animateSpeed = 1.0f;
		growthRate = 0.1f;

		returnList.Add (new Module ('F', 0, 1, 0.1f));
		returnList.Add (new Module ('a', 0, 1, 0));

		productions.Add ('a', new List<Module> ());
		//productions ['a'].Add (new Module ('!', 0, 1, initialBranchWidth));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, 1, branchAngleB));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module ('b', 0, 1, 0));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, 1, divergenceAngle));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module ('a', 0, 1, 0));

		productions.Add ('b', new List<Module> ());
		//productions ['b'].Add (new Module ('!', 0, 1, initialBranchWidth));
		productions ['b'].Add (new Module ('[', 0, -1, 0));
		productions ['b'].Add (new Module ('^', 0, 1, branchAngleB));
		productions ['b'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['b'].Add (new Module ('c', 0, 1, 0));
		productions ['b'].Add (new Module (']', 0, -1, 0));
		productions ['b'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['b'].Add (new Module ('c', 0, 1, 0));

		productions.Add ('c', new List<Module> ());
		//productions ['c'].Add (new Module ('!', 0, 1, initialBranchWidth));
		productions ['c'].Add (new Module ('[', 0, -1, 0));
		productions ['c'].Add (new Module ('*', 0, 1, branchAngleC));
		productions ['c'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['c'].Add (new Module ('b', 0, 1, 0));
		productions ['c'].Add (new Module (']', 0, -1, 0));
		productions ['c'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['c'].Add (new Module ('b', 0, 1, 0));
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
				return growthRate*(gp)*(1 - (time/1.0f));
			break;
			case '!':
				return growthRate*(gp)*(1 - (time/1.0f));
			break;
			default:
				return 0.0f;
			break;
		}
	}
}
