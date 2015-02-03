using UnityEngine;
using Vectrosity;
using System.Collections;
using System.Collections.Generic;

public class Parametric_Turtle : Turtle 
{

	private Parametric_L_System p_l_sys;
	Dictionary <int, List<Module>> additionsList;
	private Object[] possibleFlowers, possibleLeaves;

	private List<GameObject> plantParts;

	// Use this for initialization
	void Start () 
	{
		initialPosition = new Vector3 (transform.localPosition.x, 
		                               transform.localPosition.y, 
		                               transform.localPosition.z);
		
		p_l_sys = this.GetComponentInParent<Parametric_L_System> ();
		
		branchStack = new Stack ();
		lineList = new List<VectorLine> ();
		additionsList = new Dictionary<int, List<Module>>();

		possibleFlowers = Resources.LoadAll ("Prefabs/L_Systems/Flowers");
		possibleLeaves = Resources.LoadAll ("Prefabs/L_Systems/Leaves");

		plantParts = new List<GameObject> ();
	}

	void Update()
	{
		if(Input.GetButton("Grow"))
			animate = true;
		else
			animate = false;
		if(animate)	
		{
			destroyLines();
			stir (p_l_sys.returnList);
		}
	}

	public void destroyLines()
	{
		VectorLine.Destroy (lineList);
		foreach(GameObject go in plantParts)
			GameObject.Destroy(go);
		plantParts.Clear ();
	}

	void stir(List<Module> word)
	{
		transform.localPosition = new Vector3(initialPosition.x,
		                                      initialPosition.y,
		                                      initialPosition.z);

		
		if(animate)
		{
			Quaternion undoIt = Quaternion.FromToRotation(lastForward, p_l_sys.transform.forward);
			undoIt = Quaternion.Inverse (undoIt) * Quaternion.Inverse (undoIt);
			transform.rotation *= undoIt;
			lastForward = p_l_sys.transform.forward;
		}
		
		saveOrientation = new Quaternion(transform.rotation.x,
		                                 transform.rotation.y,
		                                 transform.rotation.z,
		                                 transform.rotation.w);

		foreach(Module mod in word)
		{
			if(mod.age > mod.terminalAge && mod.terminalAge > 0)
			{
				if(p_l_sys.productions.ContainsKey(mod.symbol))
				{
						List<Module> newList = new List<Module>();
						foreach(Module m in p_l_sys.productions [mod.symbol])
						{
							newList.Add(new Module(m));
							foreach(int k in p_l_sys.productions.Keys)
							{
								if(m.symbol == k)
									m.agePerturb();
								if(m.symbol == '&' && p_l_sys is rosetteInteractable)
								{
									rosetteInteractable rI = p_l_sys as rosetteInteractable;
									m.growthParameter -= rI.leafAxisShift;
									
								}
							}
						}
						additionsList.Add(word.IndexOf(mod), newList);
				}
			}

			switch(mod.symbol)
			{
				case 'F':
				{
					VectorLine line;
					float length = p_l_sys.growthFunction(mod.symbol, mod.growthParameter, mod.age);
					if(length < 0)
						mod.age = mod.terminalAge;
					else
						mod.growthParameter += length;
					
					Vector3 heading = mod.growthParameter * transform.up;
					
					line = VectorLine.SetLine3D(Color.green, transform.localPosition, 
					                            transform.localPosition + heading);
					
					line.drawTransform = p_l_sys.transform;
					//line.SetWidth(p_l_sys.getBranchWidth());
					line.Draw3DAuto();
					
					lineList.Add(line);						
					
					transform.localPosition += heading;
				}
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
				case '&':
				{
				transform.Rotate(new Vector3
				                 (p_l_sys.growthFunction(mod.symbol, mod.growthParameter, mod.age), 0, 0));
				}
					break;
				case '^':
				{
				transform.Rotate(new Vector3
				                 (-p_l_sys.growthFunction(mod.symbol,mod.growthParameter, mod.age), 0, 0));
				}
					break;
				case '+':
				{
				transform.Rotate(new Vector3
				                 (0, p_l_sys.growthFunction(mod.symbol,mod.growthParameter, mod.age), 0));
				}	
					break;
				case '-':
				{ 
				transform.Rotate(new Vector3
				                 (0, -p_l_sys.growthFunction(mod.symbol,mod.growthParameter, mod.age), 0));
				}	
					break;
				case '%':
				{
				transform.Rotate(new Vector3(0, 0, 
				                             -p_l_sys.growthFunction(mod.symbol,mod.growthParameter, mod.age)));
				}	
					break;
				case '*':
				{
					transform.Rotate(new Vector3(0, 0, 
				                             p_l_sys.growthFunction(mod.symbol,mod.growthParameter, mod.age)));
				}	
					break;
				case '!':
				{	
					float widthIncrease = p_l_sys.growthFunction(mod.symbol, mod.growthParameter, mod.age);
					if(widthIncrease < 0)
						mod.age = mod.terminalAge;
					else
						mod.growthParameter += widthIncrease; 
					
					p_l_sys.setBranchWidth(mod.growthParameter);
				}
					break;
				case 'L':
				{
					GameObject leaf = (GameObject)Instantiate(possibleLeaves[p_l_sys.sendLeafIndex()],
					                                            transform.position, 
					                                            transform.rotation);
					mod.growthParameter += p_l_sys.growthFunction(mod.symbol, mod.growthParameter, mod.age);
					leaf.transform.localScale = Vector3.one * mod.growthParameter;
					leaf.transform.parent = p_l_sys.transform;
					plantParts.Add(leaf);
				}
					break;
				case 'I':
				{
					GameObject flower = (GameObject)Instantiate(possibleFlowers[p_l_sys.sendFlowerIndex()],
					                                            transform.position, 
					                                            transform.rotation);
					mod.growthParameter += p_l_sys.growthFunction(mod.symbol, mod.growthParameter, mod.age);
					flower.transform.localScale = Vector3.one * mod.growthParameter;
					flower.transform.parent = p_l_sys.transform;
					plantParts.Add(flower);
				}
					break;
				default:
				break;
			}

			//Age the module
			mod.age += Time.deltaTime * p_l_sys.animateSpeed;
		}
		if(additionsList.Count != 0)
		{
			addNewProductions();
		}

		transform.rotation = new Quaternion(saveOrientation.x, 
		                                    saveOrientation.y,
		                                    saveOrientation.z,
		                                    saveOrientation.w);
	}

	void addNewProductions()
	{
		int i = -1;

		foreach(KeyValuePair<int, List<Module>> kvp in additionsList)
		{
			if(i == -1)
			{
				i = kvp.Value.Count - 1;
				p_l_sys.returnList.RemoveAt(kvp.Key);
				p_l_sys.returnList.InsertRange(kvp.Key, kvp.Value);
			}
			else
			{
				p_l_sys.returnList.RemoveAt(i+kvp.Key);
				p_l_sys.returnList.InsertRange(i+kvp.Key, kvp.Value);
				i += kvp.Value.Count - 1;
			}
		}
		additionsList.Clear ();
	}
}
