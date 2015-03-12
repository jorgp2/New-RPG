using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;

public class RuntimeScript : MonoBehaviour{

	Thread workerThread;
	public string[] RawLines;

	public byte[] PackedScript; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitilaizeScript(string FileName)
	{
		StreamReader Reader = new StreamReader(Application.dataPath+"\\" + FileName);
		string All = Reader.ReadToEnd ();
		RawLines = All.Split ((char) 13);
		workerThread = new Thread ( new ThreadStart(PackScript));
		workerThread.Start ();
	}

	public void PackScript()
	{

	}
}

public struct ScriptLookUpTable
{
	byte[] Instructions = {0};
}
