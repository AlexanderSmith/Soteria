using UnityEngine;
using System.Collections;

public class SceneHarborRespawn : Scene 
{
	/**
	private GameEnums.SceneName sceneName;
	private GameEnums.SceneFlags sceneState;
	**/
	
	
	const SceneFlags FailedTheater  	= SceneFlags.FLAG_1;
	const SceneFlags FailedObservatory	= SceneFlags.FLAG_2;
	const SceneFlags FailedMusic		= SceneFlags.FLAG_3;

	void Awake()
	{
		this.sceneName = SceneName.HARBOR_RESPAWN;
	}
	// all 3 fail, dying
	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}
