using UnityEngine;
using System.Collections;

public enum WeaponType {Melee, Range, Magic, Rifle};
public enum FireType {Auto, SemiAuto, Single};

[System.Serializable]
public class WeaponInfo{
	
	public WeaponType Type;
	public int Damage=5;
	public int Clips = 3;
	public int ClipSize = 30;
	public int Rounds = 90;
	public int currentClip=30;
	
	public FireType Ftype;
	public float FireRate=.1f;
	public float ReloadTime=2;
}

[System.Serializable]
public class WeaponVars{
	public Vector3 offset;
	public Vector3 rotationOffset;
	
	public GameObject ScopeCamera;
	public bool ScopeFocused=false;
	public GameObject ScopePosition;
	public Transform CameraPosition;
	public AudioSource FireSound;
	public AudioSource Empty;
	public AudioSource ReloadClick;
	public Transform Muzzle;
	public GameObject MuzzleFlash;
	public GameObject Bullet;
}

public class Weapon : Item {


	public WeaponInfo inf;
	public WeaponType Type;
	public WeaponVars WVars;


	public bool CanShoot=true;

	void Start () {
		WVars.ScopeCamera = GameObject.Find ("Scope Camera");
		itemType = ItemType.Weapon;
	}
	
	
	void Update () {
	
	}

	public IEnumerator Attack()
	{
		switch (Type) {
			case WeaponType.Rifle:
				if (inf.Rounds > 0)
					StartCoroutine(Shoot ());
			break;
			case WeaponType.Melee:
			if (inf.Rounds > 0)
				StartCoroutine(SwingWeapon());
			break;
		}
	}

	public IEnumerator SwingWeapon(){

	}

	public IEnumerator Shoot()
	{  

		switch (inf.Ftype) {
				case FireType.Auto:
					while (  Input.GetButton("Fire1") &&  inf.currentClip > 0 && CanShoot) 
					{
				if (WVars.Muzzle != null && WVars.MuzzleFlash != null)
					(Instantiate (WVars.MuzzleFlash, WVars.Muzzle.position, WVars.Muzzle.rotation)as GameObject).transform.parent = GameObject.Find ("_GeneratedCrap").transform;
				if (WVars.FireSound != null)
							WVars.FireSound.Play ();
				if (WVars.Bullet != null)
					(Instantiate (WVars.Bullet, WVars.Muzzle.position, WVars.Muzzle.rotation) as GameObject).transform.parent = GameObject.Find ("_GeneratedCrap").transform;
						inf.Rounds--;
						inf.currentClip--;
						yield return new WaitForSeconds(inf.FireRate);
					}
					if (inf.currentClip <=0 && CanShoot) {
				if(WVars.Empty != null)
					WVars.Empty.Play ();
						if(inf.Rounds>0)
							StartCoroutine (Reload (inf.ReloadTime));
						
						yield return new WaitForEndOfFrame();
					}
						break;
					case FireType.Single:
						if(inf.currentClip > 0 && CanShoot)
						{
				if (WVars.Muzzle != null && WVars.MuzzleFlash != null)
					(Instantiate (WVars.MuzzleFlash, WVars.Muzzle.position, WVars.Muzzle.rotation)as GameObject).transform.parent = GameObject.Find ("_GeneratedCrap").transform;
				if (WVars.FireSound != null)
					WVars.FireSound.Play ();
				if (WVars.Bullet != null)
					(Instantiate (WVars.Bullet, WVars.Muzzle.position, WVars.Muzzle.rotation) as GameObject).transform.parent = GameObject.Find ("_GeneratedCrap").transform;
							inf.Rounds--;
							inf.currentClip--;
							yield return new WaitForSeconds(inf.FireRate);
						}

						if (inf.currentClip <=0 && CanShoot) {
				if(WVars.Empty != null)
					WVars.Empty.Play ();
							if(inf.Rounds>0)
								StartCoroutine (Reload (inf.ReloadTime));
							
							yield return new WaitForEndOfFrame();
						}
					break;
				}

		yield return new WaitForEndOfFrame();
	}

	public IEnumerator FocusScope()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled =false;
		WVars.ScopeCamera.transform.position = WVars.ScopePosition.transform.position;
		WVars.ScopeCamera.GetComponent<Camera>().enabled = true;
		WVars.ScopeFocused = !WVars.ScopeFocused;
		yield return new WaitForEndOfFrame();

	}
	
	public IEnumerator UnFocusScope()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled =true;
		WVars.ScopeCamera.GetComponent<Camera>().enabled = false;
		WVars.ScopeFocused = !WVars.ScopeFocused;
		yield return new WaitForEndOfFrame();
	}
	public IEnumerator Reload(float time)
	{
		CanShoot=false;
		yield return new WaitForSeconds(time);
		if(inf.Rounds>inf.ClipSize){
			inf.Clips--;
			inf.currentClip=inf.ClipSize;
		}
		else
			inf.currentClip=inf.Rounds;
		if(WVars.ReloadClick!=null)
			WVars.ReloadClick.Play();
		CanShoot=true;
	}

	public void DrawHud(){
		GUI.Label(new Rect(Screen.width*.8f,Screen.height*.8f,64,32),"" + inf.currentClip+"-"+(inf.Clips-1)* inf.ClipSize);
	}

}
