using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parametric_L_System : MonoBehaviour {

	public List<Module> returnList;
	public Dictionary <char, List<Module>> productions;

	public float animateSpeed;
	public float growthRate;

	protected float currentBranchWidth;
	protected float initialSegmentLength = 0.1f;
	protected float initialFlowerSize = 0.01f;

	protected float branchAngle = 45.0f;

	// Use this for initialization
	void Start () 
	{
		returnList = new List<Module> ();
		productions = new Dictionary<char, List<Module>> ();
		animateSpeed = 1.0f;
		growthRate = 1.0f;
		currentBranchWidth = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual float growthFunction(char sym, float gfi, float time)
	{
		return 0;
	}	

	public virtual int sendFlowerIndex()
	{
		return 0;
	}

	public virtual int sendLeafIndex()
	{
		return 0;
	}

	public void setBranchWidth(float bw)
	{
		currentBranchWidth = bw;
	}
	
	public float getBranchWidth()
	{
		return currentBranchWidth;
	}
}

public class Module
{
	public char symbol;
	public float age, terminalAge, growthParameter;
	//Age, TerminalAge for production application, and a growth function identifier

	public Module(char sym, float a, float term, float gp)
	{
		symbol = sym;
		age = a;
		terminalAge = term;
		growthParameter = gp;
	}

	public Module(Module m)
	{
		symbol = m.symbol;
		age = m.age;
		terminalAge = m.terminalAge;
		growthParameter = m.growthParameter;
	}

	public virtual void agePerturb()
	{
		terminalAge += Random.Range (-0.5f, 0.5f);
	}
}


