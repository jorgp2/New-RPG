using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public Canvas thisCanvas;
	public bool paused=false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (paused && Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape)) 
		{
			thisCanvas.gameObject.SetActive(true);
			paused=!paused;
		}
		else if(Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape))
		{
			thisCanvas.gameObject.SetActive(false);
			paused=!paused;
		}
	}

	public void Pause(){
	thisCanvas.gameObject.SetActive(false);
			paused=!paused;
	}
}
