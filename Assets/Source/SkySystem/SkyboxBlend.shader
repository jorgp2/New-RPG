Shader "MyShaders/SkyboxBlend" 
{
	Properties 
	{
		_Cube ("Environment Map 1", Cube) = "white" {}
		_Cube2 ("Environment Map 2", Cube) = "white" {}
		_Blend ("Blend", Range(0.0,1.0)) = 0.5
		_Tint ("Tint Color", Color) = (.5, .5, .5, .5)
		_TintBottom ("Tint Bottom", color) =(.5, .5, .5, .5)
		_Emmision ("Sky Emmision", color) =(0,0,.8)
	}
 
	SubShader 
	{
		Tags { "Queue"="Background"  }
		Cull Off
		ZWrite off
		Fog  {Mode off} //{ Mode Exp2 color [_FogColor]} 
		Lighting off
		
		       
		Color [_Tint]
 		
		Pass 
		{
			SetTexture [_Cube] { combine texture }
			SetTexture [_Cube2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
			SetTexture [_Cube2] { combine previous +- primary, previous * primary }
		}
		
		//Pass
		//{
			//Lighting on
		// Material
		// {
			//Ambient (1,1,1)
			//Diffuse [_Tint]
			//Specular (0,0,0)
			//Emission [_Emmision]
		// }
		//}
		
		
	} 
	SubShader 
	{
		Tags { "Queue"="Background"  }
		Cull Off
		Fog { Mode Linear Color [_FogColor]}
		Lighting Off        
		Color [_Tint]
 
		Pass 
		{
			SetTexture [_Cube] { combine texture }
			SetTexture [_Cube2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
			SetTexture [_Cube2] { combine previous +- primary, previous * primary }
		}
	} 	
}