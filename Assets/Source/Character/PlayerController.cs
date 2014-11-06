using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]



public class PlayerController : MonoBehaviour {

	public Camera camera;
	public NavMeshAgent NavAgent;
	public Animator Anim;
	public WeaponManager Weap;
    public float MaxDistance = 5f;

	public Inventory Inv = new Inventory ();
	public GameObject Target;

	public int Health=30;
	public int Magic=20;
	public int XP=0;

	public bool CanMove=true;

	public GameObject Sparks;
	public GameObject YellowSparks;
	public GameObject EnemySparks;
	// Use this for initialization
	void Start () {
		if(NavAgent==null)
			NavAgent=GetComponent<NavMeshAgent>();
		StartCoroutine(DoInput());
	}
	void OnGUI(){
		GUI.Box(new Rect(Screen.width * .1f, Screen.height * .8f, Screen.width * .2f, Screen.height * .1f), "HP : " + Health + "\nMP : " + Magic + " \nXp : " + XP);
		if(!Inv.Draw && GUI.Button(new Rect( Screen.width * .85f, Screen.height * .85f, Screen.width * .1f, Screen.height * .05f),"Inventory"))
			Inv.Draw=true;
		if(Inv.Draw==true)
			Inv.DrawInventory();
		if(Anim==null)
			Anim=GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator DoInput()
	{
		while(true)
			if(Time.timeScale>0)
			{
				if(Input.GetButton("Interact"))
				{
				Debug.Log("Interact");
					StartCoroutine(ProcessMouseClick());
				}
				
				if(Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && NavAgent.remainingDistance<NavAgent.stoppingDistance)
				Anim.Play("Idle");
			yield return new WaitForSeconds(.05f);
			}
			else
				yield return new WaitForEndOfFrame();
	}
	

	public void DoDamage(int dmg){
		if(Health-dmg>0)
		{
			Health-=dmg;
		}
		else
			Application.LoadLevel(0);
	}


	IEnumerator ProcessMouseClick()
	{
		RaycastHit Test;
		if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out Test))
		{
			GameObject obj= Test.collider.gameObject;
			switch (Test.collider.tag) {
				case "Ground":
				StopCoroutine("FollowTarget");
					if (CanMove && !Inv.Draw) {
						if(Sparks!=null)
						{
							(Instantiate(Sparks,Test.point,Quaternion.Euler(new Vector3(0,0,0))) as GameObject).transform.parent=GameObject.Find("_GeneratedCrap").transform;
						}
						Anim.Play("Walk");
						NavAgent.SetDestination(Test.point);
					}
				break;
				case "Interact":

					if(Sparks!=null)
					{
						(Instantiate(Sparks,Test.point,Quaternion.Euler(new Vector3(0,0,0))) as GameObject).transform.parent=GameObject.Find("_GeneratedCrap").transform;
					}
					CanMove=false;
					//Time.timeScale=0;
					obj.GetComponent<Interact>().MouseAction(this,Input.mousePosition);
				break;
				case "Prop":
					if(Vector3.Distance(transform.position,obj.transform.position)<5)
					{
						if(YellowSparks!=null)
						(Instantiate(YellowSparks,Test.point,Quaternion.Euler(new Vector3(0,0,0))) as GameObject).transform.parent=GameObject.Find("_GeneratedCrap").transform;
						//transform.LookAt(obj.transform.position);
						obj.GetComponent<Destructible>().DoDamage(20);
						Anim.Play("SwingSword");
					}
				break;
				case "Enemy":
					if(Vector3.Distance(transform.position,obj.transform.position)<5)
					{
							if(EnemySparks!=null)
							(Instantiate(EnemySparks,Test.point,Quaternion.Euler(new Vector3(0,0,0))) as GameObject).transform.parent=GameObject.Find("_GeneratedCrap").transform;
							
					//transform.LookAt(obj.transform.position);
							//Weap.Attack(obj,Anim);
					}
					
				break;
			}
		}
		yield return new WaitForEndOfFrame();
	}
}
