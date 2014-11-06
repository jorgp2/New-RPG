using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public bool Draw;
    public ArrayList inventory = new ArrayList();
    public void DrawInventory()
    {
        if (Draw)
        {
            int c = 0;
            GUI.Box(new Rect(Screen.width * .1f, Screen.height * .1f, Screen.width * .8f, Screen.height * .8f), "Chest");
            for (int y = 0; y <( inventory.Count / 4)+1; y++)
            {
               	for (int x = 0; x < 4; x++)
                {

                    if (c<inventory.Count) {
                    	Item item = inventory[c] as Item;
                    	if (item != null)
                    	    if (item.DrawItem((Screen.width * .15f) + (x * (Screen.width * .15f)), (Screen.height * .15f) + (y * (Screen.height * .2f)), Screen.width * .15f, Screen.height * .15f))
                    	    {
                    	        inventory.RemoveAt(c);
                    	        c--;
                    	    }
                    	c++;
                    }
                }
            }

            
            if (GUI.Button(new Rect(Screen.width * .8f, Screen.height * .8f, Screen.width * .1f, Screen.height * .05f), "Close"))
            {
                Draw = false;
                Time.timeScale = 1;
            }
        }
    }
}
