using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class Turtle : MonoBehaviour {
	//To emulate the spatial variables we have for the c++ version of the turtle we attach the turtle
	//script to an empty game object. From this we receive a transform, meaning a world position and rotation.
	//We do however still need to specify intial orientation and position
	
	private Stack branchStack;
	private List<VectorLine> lineList;

	private L_System l_sys;	

	private Vector3 initialPosition;
	private Quaternion saveOrientation;
	
	public bool animate = false;

	private Vector3 lastForward;

	class TransformHolder 
	{
		public Vector3 position; 
		public Quaternion rotation; 
		
		public TransformHolder()
		{
			position = new Vector3();
			rotation = new Quaternion();
		}
		
		public void Store(Transform t)
		{
			position.Set(t.localPosition.x, t.localPosition.y, t.localPosition.z);
			rotation.Set(t.localRotation.x, t.localRotation.y, t.localRotation.z, t.localRotation.w);
		}
		
		public void Print()
		{
			Debug.Log (position + "\n");
			Debug.Log (rotation + "\n");
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		initialPosition = new Vector3 (transform.localPosition.x, 
		                               		transform.localPosition.y, 
		                               			transform.localPosition.z);

		l_sys = this.GetComponentInParent<L_System> ();

		lastForward = l_sys.transform.forward;

		if(l_sys.GetType().BaseType == typeof(L_System_Animated))
			animate = true;

		l_sys.propogateSymbols (l_sys.generations);

		branchStack = new Stack ();
		lineList = new List<VectorLine> ();

		if(!animate)
			stir (l_sys.returnList);	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void LateUpdate()
	{
		if(animate)
		{
			if(!l_sys.stop)
			{					
				destroyLines();
				stir (l_sys.returnList);
			}
		}
	}

	public void destroyLines()
	{
		VectorLine.Destroy (lineList);
	}
	
	void stir(string symbols)
	{
		transform.localPosition = new Vector3(initialPosition.x,
		                                      		initialPosition.y,
		                                      			initialPosition.z);

		if(animate)
		{
			Quaternion undoIt = Quaternion.FromToRotation(lastForward, l_sys.transform.forward);
			undoIt = Quaternion.Inverse (undoIt) * Quaternion.Inverse (undoIt);
			transform.rotation *= undoIt;
			lastForward = l_sys.transform.forward;
		}

		saveOrientation = new Quaternion(transform.rotation.x,
		                                    transform.rotation.y,
		                                    transform.rotation.z,
		                                    transform.rotation.w);

		float theta = l_sys.angle;
		float edge = l_sys.edgeLength;

		for(int i = 0; i < symbols.Length; i++)
		{
			switch(symbols[i])
			{
				case '&':
					transform.Rotate(new Vector3(theta, 0, 0));
					break;
				case '^':
					transform.Rotate(new Vector3(-theta, 0, 0));
					break;
				case '+':
					transform.Rotate(new Vector3(0, theta, 0));
					break;
				case '-':
					transform.Rotate(new Vector3(0, -theta, 0));
					break;
				case '%':
					transform.Rotate(l_sys.transform.forward, theta);
					break;
				case '*':
					transform.Rotate(l_sys.transform.forward, -theta);
					break;
				case '|':
					transform.Rotate(new Vector3(0, 180, 0));
					break;
				case '[':
				{
					TransformHolder pushTrans = new TransformHolder(); 
					pushTrans.Store(transform);
					branchStack.Push(pushTrans);
				}
					break;				
				case ']':					
				{ 
					TransformHolder popTrans = (TransformHolder)branchStack.Pop();
					transform.localPosition = new Vector3(popTrans.position.x, popTrans.position.y, popTrans.position.z);
					transform.localRotation = new Quaternion(popTrans.rotation.x, popTrans.rotation.y, popTrans.rotation.z, popTrans.rotation.w);
				}
					break;							
				case 'F':
				{
					VectorLine line;
					Vector3 heading = l_sys.edgeLength * transform.up;

					line = VectorLine.SetLine3D(Color.green, transform.localPosition, 
					                     transform.localPosition + heading);

					line.drawTransform = l_sys.transform;
					line.Draw3DAuto();

					lineList.Add(line);						
					transform.localPosition += heading;
				}
					break;
				case 'L':
				{
					//have to instantiate leaves here
				}
					break;
				case 'I':
				{
					//have to instantiate flowers now
					GameObject flower = (GameObject)Instantiate(Resources.Load("Prefabs/L_Systems/Flowers/flower2"),
				                                                         transform.position, 
				                                                         transform.rotation);
					flower.transform.parent = l_sys.transform;
				}
					break;
			}
		}
		transform.rotation = new Quaternion(saveOrientation.x, 
		                                    saveOrientation.y,
		                                    saveOrientation.z,
		                                    saveOrientation.w);
	}
}