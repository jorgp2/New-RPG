using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {

	public GUIContent con;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
