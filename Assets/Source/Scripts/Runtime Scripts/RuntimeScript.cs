using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;


public class RuntimeScript : MonoBehaviour{

	Thread workerThread;
	

	public ushort[] PackedScript; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitilaizeScript(string FileName)
	{
        PackScriptHelper MB = new PackScriptHelper();
		workerThread = new Thread ( MB.PackScript);
		workerThread.Start (FileName);
	}

	
}

public class PackScriptHelper
{
    public CodeSegements Seg;
    
    public void PackScript(object FileName)
	{
        StreamReader Reader = new StreamReader(Application.dataPath + "\\" + FileName);
        Seg = new CodeSegements( Reader.ReadToEnd().Split(new char[] { ';'}));
        IEnumerator SegEnum = Seg.GetEnumerator();
        ProcessHeader(SegEnum.Current as string);


	}

    

    void ProcessHeader(string header)
    {
        string[] xxx = header.Split(new char {':' });

        int i = 1;


    }

}

public class CodeSegements : IEnumerable
{
    public string[] Segments;

    public CodeSegements(string[] segments)
    {
        Segments = new string[segments.Length];
        for (int i = 0; i < segments.Length; i++)
        {
            Segments[i] = segments[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public CodeEnum GetEnumerator()
    {
        return new CodeEnum(Segments);
    }
}

public class CodeEnum : IEnumerator
{
    public string[] Segments;

    int position = -1;

    public CodeEnum(string[] list)
    {
        Segments = list;
    }

    public bool MoveNext()
    {
        position++;
        return (position < Segments.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public string Current
    {
        get
        {
            //try
            //{
                return Segments[position];
            //}
            //catch ( ArrayIndexOutOfRangeException)
            //{
            //    throw new  InvalidOperationException();
            //}
        }
    }
}


