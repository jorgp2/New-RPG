using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed=200f;

	public float LifeTime= 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (SelfDestruct());
		GetComponent<Rigidbody> ().velocity =transform.forward * speed;
	}

	public IEnumerator SelfDestruct(){
		yield return new WaitForSeconds(LifeTime);
		GameObject.Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
