using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sympodialInteractable : Parametric_L_System {

	// Use this for initialization
	void Start () 
	{
		returnList = new List<Module> ();
		productions = new Dictionary<char, List<Module>> ();

		branchAngle = 30.0f;
		
		animateSpeed = 1.0f;
		growthRate = 0.075f;
		
		returnList.Add (new Module ('F', 0, 1, 0.1f));
		returnList.Add (new Module ('a', 0, 1, 0));
		
		productions.Add ('a', new List<Module> ());
		//productions ['a'].Add (new Module ('!', 0, 1, intialWidth));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, branchAngle));
		productions ['a'].Add (new Module ('b', 0, 1, 0));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('*', 0, -1, branchAngle));
		productions ['a'].Add (new Module ('b', 0, 1, 0));

		productions.Add ('b', new List<Module> ());
		//productions ['a'].Add (new Module ('!', 0, 1, intialWidth));
		productions ['b'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['b'].Add (new Module ('[', 0, -1, 0));
		productions ['b'].Add (new Module ('&', 0, -1, branchAngle));
		productions ['b'].Add (new Module ('b', 0, 1, 0));
		productions ['b'].Add (new Module (']', 0, -1, 0));
		productions ['b'].Add (new Module ('%', 0, -1, branchAngle));
		productions ['b'].Add (new Module ('[', 0, -1, 0));
		productions ['b'].Add (new Module ('^', 0, -1, branchAngle));
		productions ['b'].Add (new Module ('b', 0, 1, 0));
		productions ['b'].Add (new Module (']', 0, -1, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
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
