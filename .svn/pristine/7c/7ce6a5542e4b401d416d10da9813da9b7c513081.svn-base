using UnityEngine;
using System.Collections;

public class sunScript : MonoBehaviour {

	public int sunTimer;
	public Material[] materials;

	// Use this for initialization
	void Start () 
	{
		Object[] objects = Resources.LoadAll ("Material");
		materials = new Material[3];
		materials [0] = (Material)objects [0];
		materials [1] = (Material)objects [1];
		materials [2] = (Material)objects [2];
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Every tenth frame the sun in yellow
		if(sunTimer == 20)
		{
			renderer.material = materials[2];
			sunTimer = 0;
		}
		else if(sunTimer % 2 == 0)
			renderer.material = materials[1];
		else
			renderer.material = materials[0];

		sunTimer++;
	}
}
