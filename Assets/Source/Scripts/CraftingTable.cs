using UnityEngine;
using System.Collections;

public enum CraftingTableType:int {Base,Anvil}

[System.Serializable]
public class CraftingTable : Interact {

	public CraftingTableType itemType=CraftingTableType.Base;
	public GUIContent con;
	public Vector2 TooltipPos;
	public bool showGUITooltip=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (showGUITooltip)
		{
			GUI.Box(new Rect(TooltipPos.x,TooltipPos.y, Screen.width * .2f, Screen.height * .4f), "Do you want to craft?");
			if(DrawItem(new Rect(TooltipPos.x,TooltipPos.y, Screen.width * .2f, Screen.height * .4f)))
			{
				(GameObject.Find("Crafting Menu") as GameObject).GetComponent<CraftingMenu>().showCrafting();
				showGUITooltip=false;
			}
		}
	}
	
	public virtual bool DrawItem(float x, float y, float width, float height)
	{
		//GUI.Box(new Rect(x,y,width,height),Name);
		
		return GUI.Button(new Rect(x,y,width,height),con);
	}
	public bool DrawItem(Rect pos)
	{
		return GUI.Button(pos,con);
	}
	public override void MouseAction(PlayerController play, Vector2 MousePos)
	{
		PC = play;
		showGUITooltip=true;
		TooltipPos = MousePos;
	}
}
