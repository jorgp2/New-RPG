using UnityEngine;
using System.Collections;

public class PopulateCraftingpanel : MonoBehaviour {
	public PlayerController PC;
	public GameObject thisCanvas;
	public GameObject CraftingBox;
	public ArrayList AvailableCraftingItems = new ArrayList ();
	public GameObject defaultItem;
	public bool IsDrawingPanel=false; 

	// Use this for initialization
	void Start () {

		PC = (GameObject.FindGameObjectWithTag ("Player") as GameObject).GetComponent<PlayerController>();
		UpdateAvailableItems ();
		if (PC.Inv.hasItemOfType<CraftingMaterial> ()) 
		{
			UpdateAvailableItems ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CraftingBox.gameObject.GetComponent<CraftingMenu> ().Crafting) 
		{
			UpdateAvailableItems();
			if(!IsDrawingPanel)
			{
				DrawCraftingItems();
				IsDrawingPanel=true;
			}

		}
		else if(IsDrawingPanel)
			IsDrawingPanel=false;
	}

	public void UpdateAvailableItems()
	{
		AvailableCraftingItems = PC.Inv.getItemsOfType<CraftingMaterial> ();

	}

	public void DrawCraftingItems(){
		if(AvailableCraftingItems != null && AvailableCraftingItems.Count>=0)
		for (int i = 0; i < AvailableCraftingItems.Count; i++) {
			if(AvailableCraftingItems[i]!=null)
			{
				GameObject tmp = Instantiate(defaultItem, new Vector3( -290 + (100 * i), 36, 0), Quaternion.identity)as GameObject;
				//Change the default texture, DERP.
				tmp.GetComponent<RawMaterialsBoxItem>().setUpItem(AvailableCraftingItems[i] as Item);

				tmp.transform.SetParent( thisCanvas.transform,false);
			}
		}
	}

	public void UpdatePanelView(){

	}
}
