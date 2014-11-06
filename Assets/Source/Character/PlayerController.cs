using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class CameraControls{

	public GameObject CamAxis;
	public GameObject CamY;
	public float sensitivityX = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;

	public float sensitivityY=1f;

	public float maximumY=400;
	public float minimumY=320;

	public float rotationY=0;

}


public class PlayerController : MonoBehaviour {

	public CameraControls Cam;
	public NavMeshAgent NavAgent;
	public Animator Anim;
	public WeaponManager Weap;
	public Camera cameraa;
    public float MaxDistance = 5f;
    public Transform InitialPos;

	public Inventory Inv;
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
        InitialPos = cameraa.transform;
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
				if(Input.GetButton("Click"))
				{
					StartCoroutine(ProcessMouseClick());
				}
				if(Input.GetButton("Camera"))
				{
					StartCoroutine(CamMove());
				}
				if(Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && NavAgent.remainingDistance<NavAgent.stoppingDistance)
				Anim.Play("Idle");
                if(Input.GetAxis("Horizontal")!=0 && Vector3.Distance(InitialPos.position,camera.transform.position)<MaxDistance)
                {
                    Vector3 xxxxx=camera.transform.position;

                    xxxxx+= Input.GetAxis("Horizontal")*camera.transform.right;
                    camera.transform.position = xxxxx;
                }
                if (Input.GetAxis("Vertical") != 0 && Vector3.Distance(InitialPos.position, camera.transform.position) < MaxDistance)
                {
                    Vector3 xxxxx = camera.transform.position;
                    xxxxx += Input.GetAxis("Vertical") * camera.transform.forward;
                    camera.transform.position = xxxxx;
                }
                if(Input.GetButton("CenterCamera"))
                {
                    camera.transform.position = InitialPos.position;

                }
			yield return new WaitForSeconds(.05f);
			}
			else
				yield return new WaitForEndOfFrame();
	}

	IEnumerator CamMove()
	{
		while(Input.GetButton("Camera"))
		{
			float rotationX =  Input.GetAxis("Mouse X") * Cam.sensitivityX;
			Cam.CamAxis.gameObject.transform.Rotate(Vector3.up * rotationX );

			Cam.rotationY+= -Input.GetAxis("Mouse Y") * Cam.sensitivityY;
			Cam.rotationY= Mathf.Clamp(Cam.rotationY,Cam.minimumY,Cam.maximumY);

			Vector3 tmp = Cam.CamY.transform.rotation.eulerAngles;
			tmp.z=Cam.rotationY;
			Cam.CamY.gameObject.transform.rotation=Quaternion.Euler(tmp );
			yield return new WaitForEndOfFrame();
		}
	}
	IEnumerator FollowTarget(){
		while(Target!=null)
		{
			NavAgent.SetDestination(Target.transform.position);
			if(Vector3.Distance(transform.position,Target.transform.position)<=4)
			{
				if(Target.GetComponent<EnemyManager>()!=null)
					Target.GetComponent<EnemyManager>().DoDamage(20);
				else
					Target.GetComponent<EnemyHandle>().DoDamage(20);
				Anim.Play("Attack");
			}
			yield return new WaitForSeconds(.3f);
		}
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
		if(Physics.Raycast(cameraa.ScreenPointToRay(Input.mousePosition), out Test))
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
					obj.GetComponent<Interact>().MouseAction(this);
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
							Weap.Attack(obj,Anim);
					}
					else
					{
						Target=obj;	
						StartCoroutine(FollowTarget());
					}
				break;
			}
		}
		yield return new WaitForEndOfFrame();
	}
}
