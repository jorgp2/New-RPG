using UnityEngine;
using System.Collections;

public class GunManager : MonoBehaviour {
	public Weapon SelectedWeapon;
	public Weapon[] weapons;
	public int currentWeaponIndex;
	public bool CanShoot;
	public Animation animation;
	public bool ScopeFocused;
	void Start () {
	
	}

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width*.8f,Screen.height*.8f,64,32),"" + SelectedWeapon.currentClip+"-"+(SelectedWeapon.Clips-1)* SelectedWeapon.ClipSize);
	}
	
	void Update () {
		if(Time.timeScale==0)
			return;
		if(Input.GetButtonDown("SwitchWeapon"))
		{
			if(currentWeaponIndex==0)
			{
				SelectedWeapon.gameObject.SetActive(false);
				SelectedWeapon=weapons[1];
				SelectedWeapon.gameObject.SetActive(true);
				currentWeaponIndex=1;
			}
			else
			{
				SelectedWeapon.gameObject.SetActive(false);
				SelectedWeapon=weapons[0];
				SelectedWeapon.gameObject.SetActive(true);
				currentWeaponIndex=0;
			}
		}
		if(Input.GetButtonDown("Primary"))
			StartCoroutine(Shoot());
		if(Input.GetButtonDown("Reload"))
			StartCoroutine(ReloadWeapon());
		if(Input.GetButtonDown("Scope"))
		{
			if(ScopeFocused)
			{
				ScopeFocused=false;
				SelectedWeapon.StartCoroutine("UnFocusScope");
			}
			else
			{
				ScopeFocused=true;
				SelectedWeapon.StartCoroutine("FocusScope");
			}
		}
		
	}
	IEnumerator Shoot()
	{	
		if((int)SelectedWeapon.Ftype==0)
		while(Input.GetButton("Primary"))
		{
			SelectedWeapon.Shoot();
			yield return new WaitForSeconds(SelectedWeapon.FireRate);
		}
		else if((int)SelectedWeapon.Ftype==2)
		{
			SelectedWeapon.Shoot();
			yield return new WaitForSeconds(SelectedWeapon.ReloadTime);
		}
	}
	public IEnumerator AiShoot(){
		if((int)SelectedWeapon.Ftype==0)
		{
			SelectedWeapon.Shoot();
			yield return new WaitForSeconds(SelectedWeapon.FireRate);
		}
			else if((int)SelectedWeapon.Ftype==2)
		{
			SelectedWeapon.Shoot();
			yield return new WaitForSeconds(SelectedWeapon.ReloadTime);
		}
	}
	IEnumerator ReloadWeapon()
	{
		//if(animation!=null)
			//yield return StartCoroutine (WaitForAnimation("Reload",.9f,true));
		SelectedWeapon.Reload(SelectedWeapon.ReloadTime);
			//yield return StartCoroutine(WaitForAnimation("Reload",1f,false));
		//animation.Stop("Reload");
		CanShoot=true;
		yield return new WaitForEndOfFrame();
	}
	
	IEnumerator WaitForAnimation(string name, float ratio, bool play)
	{
    //Get the animation state for the named animation
    AnimationState anim = animation[name];
    //Play the animation
    if(play) animation.Play(name);
 
    //Loop until the normalized time reports a value
    //greater than our ratio.  This method of waiting for
    //an animation accounts for the speed fluctuating as the
    //animation is played.
    while(anim.normalizedTime + float.Epsilon + Time.deltaTime < ratio)
        yield return new WaitForEndOfFrame();
	if(play)
	animation.Stop(name); 
	}
}
