using UnityEngine;
using UnityEngine.UI;


public class fieldNotebook : MonoBehaviour 
{
	private randomTextGenerator rTG;
	private Turtle2D turtle2D;

	public GameObject vectorCanvas;

	private GameObject BioButton;
	private GameObject ObjectivesButton;
	private GameObject SceneButton;
	private GameObject HideShowButton;

	// Use this for initialization
	void Start () 
	{
		//rTG = gameObject.AddComponent<randomTextGenerator> ();
		turtle2D = gameObject.AddComponent <Turtle2D> ();
		vectorCanvas = Vectrosity.VectorLine.canvas.gameObject;

		vectorCanvas.AddComponent<GraphicRaycaster> ();

		BioButton = (GameObject)Instantiate (Resources.Load ("Prefabs/UI/Buttons/BiologyInfo"));
		BioButton.transform.SetParent (vectorCanvas.transform, false);

		ObjectivesButton = (GameObject)Instantiate (Resources.Load ("Prefabs/UI/Buttons/Objectives"));
		ObjectivesButton.transform.SetParent (vectorCanvas.transform, false);

		SceneButton = (GameObject)Instantiate (Resources.Load ("Prefabs/UI/Buttons/SceneSelect"));
		SceneButton.transform.SetParent (vectorCanvas.transform, false);

		HideShowButton = (GameObject)Instantiate (Resources.Load ("Prefabs/UI/Buttons/HideShowButton"));
		HideShowButton.transform.SetParent (vectorCanvas.transform, false);

		Button button = HideShowButton.GetComponent<Button> ();
		button.onClick.AddListener (ShowHideGUI);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void ShowHideGUI()
	{
		turtle2D.HideLines ();
		SceneButton.SetActive (!SceneButton.activeSelf);
		ObjectivesButton.SetActive (!ObjectivesButton.activeSelf);
		BioButton.SetActive (!BioButton.activeSelf);
	}
}
