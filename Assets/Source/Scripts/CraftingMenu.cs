using UnityEngine;
using System.Collections;

public class CraftingMenu : MonoBehaviour {
	public Canvas thisCanvas;
	public bool Crafting=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Crafting && Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape)) 
		{
			thisCanvas.gameObject.SetActive(true);
			Crafting=!Crafting;
		}
	}

	public void showCrafting(){
		thisCanvas.gameObject.SetActive(true);
		Crafting=!Crafting;
		}

	public void hideCrafting(){
		thisCanvas.gameObject.SetActive(false);
		Crafting=!Crafting;
		}


}
