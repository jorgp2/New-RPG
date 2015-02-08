using UnityEngine;
using System.Collections;

public class RawMaterialsBoxItem : MonoBehaviour {
	public Item thisItem;
	public UnityEngine.UI.Image thisImage;

	// Use this for initialization
	void Start () {
	
	}
	public void setUpItem(Item x){
		thisImage.sprite = Sprite.Create(thisItem.con.image as Texture2D,new Rect(0,0,thisItem.con.image.width,thisItem.con.image.height),new Vector2(0,0));
	}
	// Update is called once per frame
	void Update () {
	
	}
}
