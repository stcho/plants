using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rosetteInteractable : Parametric_L_System {

	private float centralRotation = 60.0f;
	public float leafAxisShift = 0.2f;
	private float flowerFertilizer = 1.1f;

	// Use this for initialization
	void Start () 
	{
		returnList = new List<Module> ();
		productions = new Dictionary<char, List<Module>> ();
		
		branchAngle = 80.0f;
		initialFlowerSize = 0.01f;

		animateSpeed = 1.0f;
		growthRate = 0.1f;

		returnList.Add (new Module ('a', 0, 1, 0));
		returnList.Add (new Module ('F', 0, 1, 0.1f));
		returnList.Add (new Module ('I', 0, 1, initialFlowerSize));


		productions.Add ('a', new List<Module> ());
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, 1));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, -1, centralRotation));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, 1));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, -1, centralRotation));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, 1));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, -1, centralRotation));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, 1));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, -1, centralRotation));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, 1));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, -1, centralRotation));
		productions ['a'].Add (new Module ('[', 0, -1, 0));
		productions ['a'].Add (new Module ('&', 0, -1, 1));
		productions ['a'].Add (new Module ('F', 0, 1, initialSegmentLength));
		productions ['a'].Add (new Module (']', 0, -1, 0));
		productions ['a'].Add (new Module ('+', 0, -1, centralRotation));
		productions ['a'].Add (new Module ('a', 0, 1, 0));
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override float growthFunction(char sym, float gp, float time)
	{
		if(time > 1)
			time = 1;
		switch(sym)
		{
		case 'F':
			return growthRate*(gp)*(1 - (time/1.0f));
			break;
		case '!':
			return growthRate*(gp)*(1 - (time/1.0f));
			break;
		case '&':
			return gp*branchAngle*(time/1.0f);
			break;
		case 'I':
			return flowerFertilizer*growthRate*(gp)*(1 - (time/1.0f));
			break;
		default:
			return gp;
			break;
		}
	}

	//This system uses flower1
	public override int sendFlowerIndex()
	{
		return 0;
	}
}
