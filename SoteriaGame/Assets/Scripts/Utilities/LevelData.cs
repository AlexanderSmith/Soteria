using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		if (GameObject.Find("MCP") == null)
			Instantiate(Resources.Load("Prefabs/MCP"), Vector3.zero, Quaternion.identity);
		
		if (GameObject.Find("UI") == null)
			Instantiate(Resources.Load("Prefabs/UI"), Vector3.zero, Quaternion.identity);
		
		Debug.Log("Awake");	
		DontDestroyOnLoad(this);
	}
	void OnLevelWasLoaded(int level) 
	{	
		Debug.Log("SceneLoad");
		//Add Data and fuctions based on the scene getting loaded (the build order in unity  rapresents the number) 
		switch(level)
		{
			case 0:
				LoadHarborLevel();
			break;
			
			case 1:
				LoadHubLevel();	
			break;
			
			case 2:
				//Obs...etc
			break;
			
			///Keep Doing cases for every Scene
		}   
    }
	
	void LoadHarborLevel()
	{
		Debug.Log("PEWPEWPEW");
	}
	void LoadHubLevel()
	{
		Debug.Log("POWPOWPOW");
	}
	
	
}
