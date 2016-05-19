using UnityEngine;
using System.Collections;

public class SceneHarbor : Scene 
{
	/**
	private GameEnums.SceneName sceneName;
	private GameEnums.SceneFlags sceneState;
	**/
	
	void Awake()
	{
		this.sceneName = SceneName.HARBOR;
		ProgressionManager.LoadScene( this.sceneName );
	}
	
	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}