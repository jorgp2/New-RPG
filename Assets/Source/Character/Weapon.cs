﻿using UnityEngine;
using System.Collections;

public enum WeaponType {Melee, Range, Magic};
public enum FireType {Auto, SemiAuto, Single};

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

public class Weapon : Item {

	public WeaponInfo inf;
	public WeaponType Type;

	
	public GameObject ScopePosition;
	public Transform CameraPosition;
	public AudioSource FireSound;
	public AudioSource Empty;
	public AudioSource ReloadClick;
	public Transform Muzzle;
	public GameObject MuzzleFlash;
	public GameObject Bullet;
	public bool CanShoot=true;
	void Start () {
	
	}
	
	
	void Update () {
	
	}
	public void Shoot()
	{
		if(inf.currentClip>0 && CanShoot)
		{
			if(Muzzle != null && MuzzleFlash != null)
				Instantiate(MuzzleFlash,Muzzle.position,Muzzle.rotation);
			if(FireSound != null)
				FireSound.Play();
			if(Bullet!=null)
				(Instantiate(Bullet,Muzzle.position,Muzzle.rotation) as GameObject).transform.parent=GameObject.Find("GeneratedCrap").transform;
			inf.Rounds--;
			inf.currentClip--;
		}
		else if(CanShoot)
			if(Empty!=null)
			{
				Empty.Play();
			StartCoroutine(Reload(inf.ReloadTime));
			}
	}
	public IEnumerator FocusScope()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled =false;
		ScopePosition.camera.enabled=true;
		yield return new WaitForEndOfFrame();
	}
	
	public IEnumerator UnFocusScope()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled =true;
		ScopePosition.camera.enabled=false;
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
		if(ReloadClick!=null)
			ReloadClick.Play();
		CanShoot=true;
	}

	public void DrawHud(){
		GUI.Label(new Rect(Screen.width*.8f,Screen.height*.8f,64,32),"" + inf.currentClip+"-"+(inf.Clips-1)* inf.ClipSize);
	}

}
