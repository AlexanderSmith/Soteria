using UnityEngine;
using System.Collections;

public class SceneHub4 : Scene 
{	
	const SceneFlags FailedObservatory  	= SceneFlags.HEX_10000000;
	void Awake()
	{
		this.sceneName = SceneName.HUB_4;
	}
	
	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}
