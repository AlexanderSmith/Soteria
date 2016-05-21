using UnityEngine;
using System.Collections;

public class SceneHub2 : Scene 
{
	const SceneFlags FailedMusic  	= SceneFlags.HEX_10000000;
	void Awake()
	{
		this.sceneName = SceneName.HUB_2;
	}
	
	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}
