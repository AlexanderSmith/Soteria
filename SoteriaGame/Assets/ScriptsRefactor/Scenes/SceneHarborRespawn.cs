using UnityEngine;
using System.Collections;

public class SceneHarborRespawn : Scene 
{
	/**
	private GameEnums.SceneName sceneName;
	private GameEnums.SceneFlags sceneState;
	**/
	
	
	const SceneFlags FailedTheater  	= SceneFlags.HEX_00000001;
	const SceneFlags FailedObservatory	= SceneFlags.HEX_00000010;
	const SceneFlags FailedMusic		= SceneFlags.HEX_00000100;


	
	void ProgMan::Died_UpdateHarborRespawn()
	{
		condition for How u died, (FailedTheater, etc
			setSceneflags(harborrespawn, someSpecificSceneFlag for 
		
	}

	void Awake()
	{
		this.sceneName = SceneName.HARBOR_RESPAWN;
			progMang.loadScane(this.sceneName)


	}
	// all 3 fail, dying
	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}
