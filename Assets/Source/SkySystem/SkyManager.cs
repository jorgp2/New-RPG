using UnityEngine;
using System.Collections;

public class SkyManager : MonoBehaviour {
	public float Test;
	public Gradient SkyColor;
	public AnimationCurve SkyIllum;
	public AnimationCurve SkyTrans;
	public GameObject SkyLight;
	public float MaxSkyIllum=6;

	public float TimeScale=1f;
	public float worldHours=1;
	public float worldMinutes=0;
	public float worldSeconds;
	private float TotalMinutes;
	public float UpdateInterval=.1f;


	void Start () {
		RenderSettings.fogColor=SkyColor.Evaluate(((worldHours * 0.0416666666666667f ) + (worldMinutes * 6.944444444444444e-4f)));
		RenderSettings.skybox.SetColor("_Tint" ,SkyColor.Evaluate(((worldHours * 0.0416666666666667f )+(worldMinutes * 6.944444444444444e-4f ))));
		RenderSettings.skybox.SetFloat("_Blend", SkyTrans.Evaluate(((worldHours * 0.0416666666666667f )+(worldMinutes * 6.944444444444444e-4f))));
		if(SkyLight!=null)
		{
			SkyLight.light.color=SkyColor.Evaluate(((worldHours * 0.0416666666666667f )+(worldMinutes*(1/1440))));
			SkyLight.light.intensity=SkyIllum.Evaluate(((worldHours * 0.0416666666666667f)+(worldMinutes*(1/1440))))*MaxSkyIllum;
			SkyLight.transform.rotation= Quaternion.Euler( new Vector3( -(worldHours * 15) -(worldMinutes * .25f) - 90 ,0,0));
		}
		StartCoroutine(RunSky());
		StartCoroutine(BlendSky());

	}
	

	void Update () {
	
	}
	IEnumerator BlendSky(){
		while(true)
		{
			if(Time.timeScale!=0)
			{
				if(SkyLight!=null)
				{
					SkyLight.light.color=SkyColor.Evaluate(((worldHours * 0.0416666666666667f )+(worldMinutes*(1/1440))));
					SkyLight.light.intensity=SkyIllum.Evaluate(((worldHours * 0.0416666666666667f)+(worldMinutes*(1/1440))))*MaxSkyIllum;
					SkyLight.transform.rotation= Quaternion.Euler( new Vector3( -(worldHours * 15) -(worldMinutes * .25f) - 90 ,0,0));
				}
				RenderSettings.fogColor=SkyColor.Evaluate(((worldHours * 0.0416666666666667f ) + (worldMinutes * 6.944444444444444e-4f)));
				RenderSettings.skybox.SetColor("_Tint" ,SkyColor.Evaluate(((worldHours * 0.0416666666666667f )+(worldMinutes * 6.944444444444444e-4f ))));
				RenderSettings.skybox.SetFloat("_Blend", SkyTrans.Evaluate(((worldHours * 0.0416666666666667f )+(worldMinutes * 6.944444444444444e-4f))));
				//RenderSettings.skybox.SetColor("_Emmision",SkyColor.Evaluate(TotalMinutes*.0006944444444f));
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator RunSky()
	{
		while(true)
		{
			if(Time.timeScale!=0)
			{
				//if(worldSeconds>=59 )
				//{
					worldSeconds=0;
					if(worldMinutes>=59)
					{
						worldMinutes=0;
						if(worldHours>=24)
						{
							worldHours=1;
						}
						else
							worldHours++;
					}
					else
					worldMinutes+=Time.smoothDeltaTime/TimeScale;
						//worldMinutes++;
				//}
				//else
				//	worldSeconds++;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
