using Vectrosity;
using UnityEngine;
using System.Collections;

public class tallWeedSystem : L_System {

	// Use this for initialization
	void Start () 
	{
		angle = 35.0f;
		edgeLength = 0.5f;

		productions = new ArrayList ();
		productions.Add ("F[*F]F[%F]F");

		returnList = "F";
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		lookAtCamera ();
	}
}
