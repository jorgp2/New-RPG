using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RawMaterialsBoxItem : UnityEngine.UI.Selectable {
	public Item thisItem;
	public Image thisImage;
	public bool IsSelected=false;

	// Use this for initialization
	void Start () {
		thisImage=GetComponent<UnityEngine.UI.Image>();
	}
	public void setUpItem(Item x){
		thisItem = x;
		Sprite xx = Sprite.Create (thisItem.con.image as Texture2D, new Rect (0, 0, thisItem.con.image.width, thisItem.con.image.height), Vector2.zero);
		thisImage.sprite = xx;
	}
	// Update is called once per frame
	void Update () {
		if(IsSelected)
			transform.position = Input.mousePosition;
	}

	public override void OnPointerDown (UnityEngine.EventSystems.PointerEventData eventData)
	{
		IsSelected = true;
		base.OnPointerDown (eventData);
	}
	public override void OnPointerUp (UnityEngine.EventSystems.PointerEventData eventData)
	{
		IsSelected = false;
		base.OnPointerUp (eventData);
	}

	public override void OnSelect (UnityEngine.EventSystems.BaseEventData eventData)
	{

		base.OnSelect (eventData);
	}
	public override void OnPointerEnter (UnityEngine.EventSystems.PointerEventData eventData)
	{

		base.OnPointerEnter (eventData);
	}
}
