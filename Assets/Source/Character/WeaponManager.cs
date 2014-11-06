using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	public Weapon SelectedWeapon;
	public Weapon[] Weapons;
	public bool Draw;
	
	public GameObject DmgText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (Draw) 
		{
			GUI.Box(new Rect(Screen.width * .1f, Screen.height *.1f,Screen.width*.8f,Screen.height*.8f),"weapons");
			for (int i = 0; i < Weapons.Length; i++) {
				if(Weapons[i] != null && Weapons[i].con!=null)
					if(Weapons[i].DrawItem(new Rect(new Rect(Screen.width * (.1f + .2f*i), Screen.height *.15f,Screen.width*.2f,Screen.height*.2f))))
					{
						SelectedWeapon.gameObject.SetActive(false);
						SelectedWeapon= Weapons[i];
					SelectedWeapon.gameObject.SetActive(true);
					}
			}
			Draw = !GUI.Button (new Rect(Screen.width * .8f, Screen.height *.8f,Screen.width*.1f,Screen.height*.05f),"Close");
		}
		else 
			Draw = GUI.Button (new Rect(Screen.width * .9f, Screen.height *.8f,Screen.width*.1f,Screen.height*.05f),"Weapons");
	}

	/*
	public void Attack(GameObject obj,Animator Anim){
		if(obj.GetComponent<EnemyManager>()!=null){
			GameObject tmp= Instantiate(DmgText) as GameObject;
			tmp.guiText.text="-"+SelectedWeapon.Inf.Damage;
			tmp.guiText.color=Color.blue;
			tmp.transform.parent=GameObject.Find("_GeneratedCrap").transform;
			tmp.GetComponent<GUIText>().pixelOffset=new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
			obj.GetComponent<EnemyManager>().DoDamage(SelectedWeapon.Inf.Damage);
		}
		else
		{
			GameObject tmp= Instantiate(DmgText) as GameObject;
			tmp.guiText.text="-"+SelectedWeapon.Inf.Damage;
			tmp.guiText.color=Color.blue;
			tmp.transform.parent=GameObject.Find("_GeneratedCrap").transform;
			tmp.GetComponent<GUIText>().pixelOffset=new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
			obj.GetComponent<EnemyHandle>().DoDamage(SelectedWeapon.Inf.Damage);
		}
		Anim.Play("Attack",SelectedWeapon.WeaponAnimLayerIndex);

	}
	*/
}
