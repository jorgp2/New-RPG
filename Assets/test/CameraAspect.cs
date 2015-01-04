using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {
	public Vector2 CameraAspectRatio;
	// Use this for initialization
	void Start () {
		GetComponent<Camera> ().aspect = CameraAspectRatio.x / CameraAspectRatio.y; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
