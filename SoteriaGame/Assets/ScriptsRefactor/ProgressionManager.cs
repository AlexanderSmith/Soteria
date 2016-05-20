using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
	private Dictionary< SceneName, string > 				dict_SceneToStr =	
		new Dictionary< SceneName, string >();
	private Dictionary< SceneName, SceneFlags >				dict_SceneToFlags =
		new Dictionary< SceneName, SceneFlags >();

	private SceneName	_curScene;
	private SceneFlags	_sceneState;
	
	private WorldFlags	_worldState;
	
	#region ACCESSORS/MUTATORS
	public static bool LoadScene( SceneName sceneToLoad )
	{
		if( _instance.dict_SceneToFlags.ContainsKey( sceneToLoad ) )
		{
			// Save Current Scene's Flags
			_instance.dict_SceneToFlags[ _instance._curScene ] = _instance._sceneState;

			// Set Current Scene as Scene to Load
			_instance._curScene = sceneToLoad;

			// Load *new* Current Scene
			_instance._sceneState = _instance.dict_SceneToFlags[ _instance._curScene ]; 

			return true;
		}
		else
		{
			Debug.Log( "*ERROR* ProgressionManager::LoadScene() : Can't find sceneName key in SceneToFlags Dictionary" );
			//Assert//
			return false;
		}
	}

	public static SceneFlags	curSceneState
	{
		get
		{
			return _instance._sceneState;
		}
		set
		{
			_instance._sceneState = curSceneState;
		}
	}
	public static WorldFlags	curWorldState
	{
		get
		{
			return _instance._worldState;
		}
		set
		{
			_instance._worldState = curWorldState;
		}
	}
#endregion

	// TEMPORARY - Did not know where to put init stuff for ProgMan
	#region DICTIONARY_INIT
	private void InitSceneNameDict()
	{
		dict_SceneToStr.Add( SceneName.MAIN_MENU, "MainMenu" );
		dict_SceneToStr.Add( SceneName.CREDITS, "Credits" );
		dict_SceneToStr.Add( SceneName.HARBOR, "Harbor" );
		dict_SceneToStr.Add( SceneName.HARBOR_RESPAWN, "HarborRespawn" );
		
		dict_SceneToStr.Add( SceneName.HUB_1, "HUBPass1" );
		dict_SceneToStr.Add( SceneName.HUB_2, "HUBPass2" );
		dict_SceneToStr.Add( SceneName.HUB_3, "HUBPass3" );
		dict_SceneToStr.Add( SceneName.HUB_4, "HUBPass4" );
		
		dict_SceneToStr.Add( SceneName.MUSIC_1, "MusicPass1" );
		dict_SceneToStr.Add( SceneName.MUSIC_2, "MusicPass2" );
		dict_SceneToStr.Add( SceneName.MUSIC_3, "MusicPass3" );
		dict_SceneToStr.Add( SceneName.MUSIC_4, "MusicPass4" );
		dict_SceneToStr.Add( SceneName.MUSIC_PUZZLE, "MusicPuzzle" );
		
		dict_SceneToStr.Add( SceneName.THEATER_1, "TheaterPass1" );
		dict_SceneToStr.Add( SceneName.THEATER_2, "TheaterPass2" );
		dict_SceneToStr.Add( SceneName.THEATER_3, "TheaterPass3" );
		dict_SceneToStr.Add( SceneName.THEATER_4, "TheaterPass4" );
		dict_SceneToStr.Add( SceneName.THEATER_PUZZLE, "PuppetPuzzle" );
		
		dict_SceneToStr.Add( SceneName.OBS_1, "ObservatoryPass1" );
		dict_SceneToStr.Add( SceneName.OBS_2, "ObservatoryPass2" );
		dict_SceneToStr.Add( SceneName.OBS_3, "ObservatoryPass3" );
		dict_SceneToStr.Add( SceneName.OBS_4, "ObservatoryPass4" );
		dict_SceneToStr.Add( SceneName.OBS_PUZZLE, "ObservatoryPuzzle" );
		
		dict_SceneToStr.Add( SceneName.SEWERS, "Sewers" );
		dict_SceneToStr.Add( SceneName.TUTORIAL, "Tutorial" );
	}
	
	private void InitSceneInfoDict()
	{
		dict_SceneToFlags.Add( SceneName.MAIN_MENU, SceneFlags.EMPTY_FLAG);
		dict_SceneToFlags.Add( SceneName.CREDITS, SceneFlags.EMPTY_FLAG);
		dict_SceneToFlags.Add( SceneName.HARBOR, SceneFlags.EMPTY_FLAG);
		dict_SceneToFlags.Add( SceneName.HARBOR_RESPAWN,  SceneFlags.EMPTY_FLAG);
		
		dict_SceneToFlags.Add( SceneName.HUB_1, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.HUB_2, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.HUB_3, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.HUB_4, SceneFlags.EMPTY_FLAG );
		
		dict_SceneToFlags.Add( SceneName.MUSIC_1, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.MUSIC_2, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.MUSIC_3, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.MUSIC_4, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.MUSIC_PUZZLE, SceneFlags.EMPTY_FLAG );
		
		dict_SceneToFlags.Add( SceneName.THEATER_1, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.THEATER_2, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.THEATER_3, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.THEATER_4, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.THEATER_PUZZLE, SceneFlags.EMPTY_FLAG );
		
		dict_SceneToFlags.Add( SceneName.OBS_1, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.OBS_2, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.OBS_3, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.OBS_4, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.OBS_PUZZLE, SceneFlags.EMPTY_FLAG );
		
		dict_SceneToFlags.Add( SceneName.SEWERS, SceneFlags.EMPTY_FLAG );
		dict_SceneToFlags.Add( SceneName.TUTORIAL, SceneFlags.EMPTY_FLAG );

		
		dict_SceneToFlags.Add( SceneName.NO_SCENE, SceneFlags.EMPTY_FLAG );
	}
	#endregion 
	private void Init()
	{
		// Init Dictionaries
		InitSceneInfoDict();
		InitSceneNameDict();

		// Init Scene Key
		_curScene = SceneName.NO_SCENE;

		// Init Scene States
		_sceneState = SceneFlags.EMPTY_FLAG;
		_worldState = WorldFlags.EMPTY_FLAG;
	}	
#region SINGLETON_STUFF
	private static ProgressionManager _instance{get ; private set;}
	/*
	public static ProgressionManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<ProgressionManager>();
			}
			return _instance;
		}
	}
	*/
	
	void Awake()
	{
		if( _instance && this != _instance )
		{
			Destroy( this.gameObject );
		}

		_instance = this;

		DontDestroyOnLoad(gameObject);

		_instance.Init();
	}
#endregion
	
	
}