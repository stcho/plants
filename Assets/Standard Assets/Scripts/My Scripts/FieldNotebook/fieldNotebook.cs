using UnityEngine;


public class fieldNotebook : MonoBehaviour 
{
	private randomTextGenerator rTG;
	private Turtle2D turtle2D;
	private GUIStyle notebookStyle;
	public Texture2D texture;
	public GameObject vectorCanvas;

	// Use this for initialization
	void Start () 
	{
		//rTG = gameObject.AddComponent<randomTextGenerator> ();
		turtle2D = gameObject.AddComponent <Turtle2D> ();
		notebookStyle = new GUIStyle ();
		vectorCanvas = GameObject.Find ("VectorCanvas");
		Debug.Log (vectorCanvas);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnGUI()
	{
		GUI.BeginGroup (new Rect (7 * Screen.width / 8, 3 * Screen.height / 4, Screen.width/4, 3*Screen.height/4),
		                	texture, notebookStyle);
		//GUI.Label (new Rect(Screen.width - 300, 0, 300, 100), rTG.GetText());
		GUI.EndGroup ();
	}


}
