using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	public Weapon SelectedWeapon;
	public ArrayList Weapons = new ArrayList();
	public bool Draw;

	public Transform WeaponBasePosition;
	
	public GameObject DmgText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void tryShoot(){
		if (SelectedWeapon != null && SelectedWeapon.inf.Rounds > 0)
			SelectedWeapon.Shoot ();
	}

	void OnGUI(){
		if (Draw) 
		{
			GUI.Box(new Rect(Screen.width * .1f, Screen.height *.6f,Screen.width*.8f,Screen.height*.2f),"weapons");
			for (int i = 0; i < Weapons.Count; i++) {
				if(Weapons[i] != null && (Weapons[i]as Item).con!=null)
					if((Weapons[i]as Item ).DrawItem(new Rect(new Rect(Screen.width * (.1f + .2f*i), Screen.height * .6f,Screen.width*.2f,Screen.height*.2f))))
					{
						ArmWeapon(i);
					}
			}
		}

	}
	public void ArmWeapon(int x)
	{
		Weapon tmp = Weapons [x] as Weapon;
		GameObject tmpx = tmp.gameObject;
		if (!tmpx.activeInHierarchy) 
		{
			tmpx.transform.position=WeaponBasePosition.position + tmp.offset;
			tmpx.GetComponent<Rigidbody>().isKinematic=true;
			tmpx.GetComponent<Collider>().enabled=false;
			tmpx.transform.rotation=Quaternion.Euler( WeaponBasePosition.rotation.eulerAngles + tmp.rotationOffset);
			tmpx.transform.parent=WeaponBasePosition;
			tmpx.SetActive(true);
		}
		SelectedWeapon = tmp;
	}

	public void AddWeapon(Item x){
		if (Weapons.Count == 0) {
				Weapons.Add (x as Weapon);
				ArmWeapon (0);
		} else 
		{
			Weapons.Add (x as Weapon);
		}
	
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
