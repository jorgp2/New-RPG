using UnityEngine;
using System.Collections;

public enum ItemType:int {Base,Weapon,Prop,RawMaterial}

[System.Serializable]
public class Item : Interact {

	public ItemType itemType=ItemType.Base;
	public GUIContent con;
	public Vector2 TooltipPos;
	public bool showGUITooltip=false;

	//void Start () {
	
	//}

	//void Update () {
	
	//}

	void OnGUI()
	{
		if (showGUITooltip)
		{
			GUI.Box(new Rect(TooltipPos.x,TooltipPos.y, Screen.width * .2f, Screen.height * .4f), "What to do with this item?");
			if(DrawItem(new Rect(TooltipPos.x,TooltipPos.y, Screen.width * .2f, Screen.height * .4f)))
			{
				PC.Inv.inventory.Add(this);

				showGUITooltip=false;
				gameObject.SetActive(false);
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
