using UnityEngine;
using System.Collections;

public class SceneHub3 : Scene 
{	
	const SceneFlags FailedTheater  	= SceneFlags.HEX_10000000;
	void Awake()
	{
		this.sceneName = SceneName.HUB_3;
	}
	
	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}
