using UnityEngine;
using System.Collections;
using System.IO;

public class LoadRuntimeScripts : MonoBehaviour {

	public RuntimeScript[] Scripts;
	
	// Use this for initialization
	void Start () {
		string[] ScriptNames = Directory.GetFiles (Application.dataPath+"\\"+"Data\\");
		Scripts = new RuntimeScript[ScriptNames.Length];
		for (int i = 0; i < ScriptNames.Length; i++) Scripts [i] = (new GameObject().AddComponent<RuntimeScript>()).GetComponent<RuntimeScript> (); 
		for (int i = 0; i < Scripts.Length; i++) Scripts [i].InitilaizeScript (ScriptNames[i]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
