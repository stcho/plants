using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Quite similar in function to L_System_Trail, generates juvenile L_Systems
 * in a much larger radius around the player. They are interactable (growable)
 * and senesce based on distance in much the same way as the non-interactive
 * specimens in the trail.*/

public class L_System_Field : MonoBehaviour {

	private Object[] possiblePlants;
	
	public List<GameObject> plantList;
	private List<GameObject> deadList;
	
	private float circleRadius = 50.0f;
	private float killCircle = 75.0f;
	
	private int maxPlantPopulation = 12;
	
	//Spawned plant parameters
	private float maxHeight = 0.1f;
	private float maxHeightInCircle = 0.25f;
	private float maxAngle = 45.0f;
	private float minAngle = 10.0f;
	private int maxGenerationsOnSpawn = 2;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Here");
		possiblePlants = Resources.LoadAll ("Prefabs/L_Systems/Static");
		Debug.Log ("and now here");
		plantList = new List<GameObject> ();
		deadList = new List<GameObject> ();
		Debug.Log (possiblePlants [2]);
		while(plantList.Count < maxPlantPopulation)
		{
			AddPlant();
			Debug.Log("Got: " + plantList.Count);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//L_System_Interactable_Animated lsa;


		
		//Check up on those already living
		foreach(GameObject ls in plantList)
		{
			//We kill plants immediately after they leave the circle. The kill circle is larger
			//than the spawn circle to prevent immediate death upon spawning near the edge
			Vector3 playerPos = new Vector3(transform.position.x, 0, transform.position.z);
			if(Vector3.Magnitude(ls.transform.position - playerPos) > killCircle)
			{
				deadList.Add (ls);
			}
		}
		
		//Get rid of plants too old
		foreach(GameObject ls in deadList)
		{
			AddPlant();												//Get new bodies in there
			plantList.Remove(ls);
			Turtle turtle = ls.GetComponentInChildren<Turtle>();	//Going to have to handle deleting
			turtle.destroyLines();									//models for systems with component
			Destroy(ls);											//parts. Will probably want to write a 
		}															//destruction script to handle the turtle,
																	//the lines, as well as assets like models.
		deadList.Clear ();
	}

	void AddPlant()
	{
		L_System ls;
		//Instantiate a new prefab's location using trigonometry
		//	eventually we will randomly select a prefab of different L_Systems
		int numLSystems = possiblePlants.Length;
		
		Vector2 xz = Random.insideUnitCircle * circleRadius;
		Vector3 plantPos = new Vector3 (xz.x + transform.position.x, 0, xz.y + transform.position.z);
		//plantPos.y = Terrain.activeTerrain.SampleHeight (plantPos);	for use with complex terrains
		GameObject newPlant = 
			(GameObject)Instantiate(possiblePlants[Mathf.FloorToInt(Random.Range(0, numLSystems))],
			                        plantPos, Quaternion.identity);
		
		ls = newPlant.GetComponent<L_System>();
		// -set draw parameters
		ls.edgeLength = Random.Range(0.05f, maxHeightInCircle);

		ls.angle = Random.Range(minAngle, maxAngle);
		// -set maxGenerations for propogation
		ls.generations = Mathf.FloorToInt(Random.Range(2, maxGenerationsOnSpawn));
		// -the actual value for both of these parameters 
		//	should be randomly generated within the acceptable range
		plantList.Add(newPlant);
		//Add it to the list
	}
}
