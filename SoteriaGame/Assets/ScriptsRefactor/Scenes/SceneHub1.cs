using UnityEngine;
using System.Collections;

public class SceneHub1 : Scene 
{
	/**
	private GameEnums.SceneName sceneName;
	private GameEnums.SceneFlags sceneState;
	**/

	byte OMalleyTalked  = 0x00000001;
	byte Cartographer   = 0x00000010;
	byte PT				= 0x00000100;

	public void DisableCartographer()
	{
		FlagTools.Scene_RemoveFlag( ProgressionManager., Cartographer );
	}
	
	protected void InterpretSceneState()
	{

	}
	void Awake()
	{
		this.sceneName = SceneName.HUB_1;
	}

	void Start()
	{
		//ProgMan.GetScenEFlags
	}
}
