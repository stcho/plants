using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class Turtle2D : MonoBehaviour {
	//To emulate the spatial variables we have for the c++ version of the turtle we attach the turtle
	//script to an empty game object. From this we receive a transform, meaning a world position and rotation.
	//We do however still need to specify intial orientation and position
	
	private Stack branchStack;
	public List<VectorLine> lineList;
	
	public L_System l_sys;	

	//2D coordinate, rotation float in z position
	public Vector3 details;
	
	// Use this for initialization
	void Start () 
	{	
		initializeTurtle2D ();
		details = new Vector3 (7*Screen.width / 8, Screen.height / 8, Mathf.PI/2);
		l_sys = gameObject.AddComponent<GUISystem> ();

		GUISystem guilsys = l_sys as GUISystem;
		guilsys.setParameters ();
		guilsys.propogateSymbols (guilsys.generations);
		stir (guilsys.returnList);
	}
	
	// Update is called once per frame
	void Update () 
	{}
	
	public void destroyLines()
	{
		VectorLine.Destroy (lineList);
	}

	public void initializeTurtle2D()
	{
		branchStack = new Stack ();
		lineList = new List<VectorLine> ();
	}
	
	public void stir(string symbols)
	{	
		float theta = l_sys.angle;
		float edge = l_sys.edgeLength;
		
		for(int i = 0; i < symbols.Length; i++)
		{
			switch(symbols[i])
			{
				case '%':
					details.z += theta;
					break;
				case '*':
					details.z -= theta;
					break;
				case '[':
				{
					branchStack.Push(details);
				}
					break;				
				case ']':					
				{ 
					details = (Vector3)branchStack.Pop();
				}
					break;							
				case 'F':
				{
					VectorLine line;
					Vector2 pos = new Vector2(details.x, details.y);
					Vector2 newPos = new Vector2(details.x + (edge * Mathf.Cos(details.z)),
					                             details.y + (edge * Mathf.Sin(details.z)));
					line = VectorLine.SetLine(Color.green, pos, newPos);
					
					lineList.Add(line);						
					details.x = newPos.x;
					details.y = newPos.y;
				}
				break;
			}
		}
	}
}