using UnityEngine;
using System.Collections;

public class PopulateCraftingpanel : MonoBehaviour {
	public PlayerController PC;
	public Canvas thisCanvas;
	public GameObject CraftingBox;
	public Item[] AvailableCraftingItems=new Item[0];
	public GameObject defaultItem;

	// Use this for initialization
	void Start () {
		PC = (GameObject.FindGameObjectWithTag ("Player") as GameObject).GetComponent<PlayerController>();
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
			DrawCraftingItems();
		}
	}

	public void UpdateAvailableItems()
	{
		AvailableCraftingItems = PC.Inv.getItemsOfType<CraftingMaterial> ();
	}

	public void DrawCraftingItems(){
		if(AvailableCraftingItems != null && AvailableCraftingItems.Length>=0)
		for (int i = 0; i < AvailableCraftingItems.Length; i++) {
			if(AvailableCraftingItems[i]!=null)
			{
				GameObject tmp = Instantiate(defaultItem)as GameObject;
				tmp.transform.SetParent( thisCanvas.transform,false);
			}
		}
	}

	public void UpdatePanelView(){

	}
}
