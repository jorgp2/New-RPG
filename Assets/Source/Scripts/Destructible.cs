using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {
	public int health;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public virtual void DoDamage(int i){
		if(health-i <=0)
			Destroy(this.gameObject);
		else
			health-=i;
	}
}
