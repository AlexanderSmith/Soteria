using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
	private Dictionary< SceneName, string > 				dict_SceneToStr =	
		new Dictionary< SceneName, string >();
	
	public SceneFlags Flags_MAIN_MENU;
	public SceneFlags Flags_CREDITS;
	
	public SceneFlags Flags_HARBOR;
	public SceneFlags Flags_HARBOR_RESPAWN;
	
	public SceneFlags Flags_HUB_1;
	public SceneFlags Flags_HUB_2;
	public SceneFlags Flags_HUB_3;
	public SceneFlags Flags_HUB_4;
	
	public SceneFlags Flags_MUSIC_1;
	public SceneFlags Flags_MUSIC_2;
	public SceneFlags Flags_MUSIC_3;
	public SceneFlags Flags_MUSIC_4;
	public SceneFlags Flags_MUSIC_PUZZLE;
	
	public SceneFlags Flags_THEATER_1;
	public SceneFlags Flags_THEATER_2;
	public SceneFlags Flags_THEATER_3;
	public SceneFlags Flags_THEATER_4;
	public SceneFlags Flags_THEATER_PUZZLE;
	
	public SceneFlags Flags_OBS_1;
	public SceneFlags Flags_OBS_2;
	public SceneFlags Flags_OBS_3;
	public SceneFlags Flags_OBS_4;
	public SceneFlags Flags_OBS_PUZZLE;
	
	public SceneFlags Flags_SEWERS;
	public SceneFlags Flags_TUTORIAL;


	public SceneName	CurSceneName;
	public WorldFlags	Flags_World;
	
	#region ACCESSORS/MUTATORS
	public static class AccessOther
	{
		public static class HarborRespawn
		{
			public static void ChangeSomeFlag()
			{

			}
		}
	}

	// RETURNED BY VALUE
	public static SceneFlags GetSceneFlags( SceneName inName )
	{
		switch( inName )
		{
		case SceneName.MAIN_MENU:
			return instance.Flags_MAIN_MENU;
			break;
		case SceneName.CREDITS:
			return instance.Flags_CREDITS;
			break;
		case SceneName.HARBOR:
			return instance.Flags_HARBOR;
			break;
		case SceneName.HARBOR_RESPAWN:
			return instance.Flags_HARBOR_RESPAWN;
			break;
		case SceneName.HUB_1:
			return instance.Flags_HUB_1;
			break;
		case SceneName.HUB_2:
			return instance.Flags_HUB_2;
			break;
		case SceneName.HUB_3:
			return instance.Flags_HUB_3;
			break;
		case SceneName.HUB_4:
			return instance.Flags_HUB_4;
			break;
		case SceneName.MUSIC_1:
			return instance.Flags_MUSIC_1;
			break;
		case SceneName.MUSIC_2:
			return instance.Flags_MUSIC_2;
			break;
		case SceneName.MUSIC_3:
			return instance.Flags_MUSIC_3;
			break;
		case SceneName.MUSIC_4:
			return instance.Flags_MUSIC_4;
			break;
		case SceneName.MUSIC_PUZZLE:
			return instance.Flags_MUSIC_PUZZLE;
			break;
		case SceneName.THEATER_1:
			return instance.Flags_THEATER_1;
			break;
		case SceneName.THEATER_2:
			return instance.Flags_THEATER_2;
			break;
		case SceneName.THEATER_3:
			return instance.Flags_THEATER_3;
			break;
		case SceneName.THEATER_4:
			return instance.Flags_THEATER_4;
			break;
		case SceneName.THEATER_PUZZLE:
			return instance.Flags_THEATER_PUZZLE;
			break;
		case SceneName.OBS_1:
			return instance.Flags_OBS_1;
			break;
		case SceneName.OBS_2:
			return instance.Flags_OBS_2;
			break;
		case SceneName.OBS_3:
			return instance.Flags_OBS_3;
			break;
		case SceneName.OBS_4:
			return instance.Flags_OBS_4;
			break;
		case SceneName.OBS_PUZZLE:
			return instance.Flags_OBS_PUZZLE;
			break;
		case SceneName.SEWERS:
			return instance.Flags_SEWERS;
			break;
		case SceneName.TUTORIAL:
			return instance.Flags_TUTORIAL;
			break;
		default:
			UnityEngine.Assertions.Assert.AreEqual(0,0);
			break;
		}
	}

	#endregion

	// TEMPORARY - Did not know where to put init stuff for ProgMan
	#region INIT_FUNCTIONS
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
	private void InitSceneFlags()
	{
		Flags_MAIN_MENU = SceneFlags.EMPTY_FLAG;
		Flags_CREDITS = SceneFlags.EMPTY_FLAG;
		
		Flags_HARBOR = SceneFlags.EMPTY_FLAG;
		Flags_HARBOR_RESPAWN = SceneFlags.EMPTY_FLAG;
		
		Flags_HUB_1 = SceneFlags.EMPTY_FLAG;
		Flags_HUB_2 = SceneFlags.EMPTY_FLAG;
		Flags_HUB_3 = SceneFlags.EMPTY_FLAG;
		Flags_HUB_4 = SceneFlags.EMPTY_FLAG;
		
		Flags_MUSIC_1 = SceneFlags.EMPTY_FLAG;
		Flags_MUSIC_2 = SceneFlags.EMPTY_FLAG;
		Flags_MUSIC_3 = SceneFlags.EMPTY_FLAG;
		Flags_MUSIC_4 = SceneFlags.EMPTY_FLAG;
		Flags_MUSIC_PUZZLE = SceneFlags.EMPTY_FLAG;
		
		Flags_THEATER_1 = SceneFlags.EMPTY_FLAG;
		Flags_THEATER_2 = SceneFlags.EMPTY_FLAG;
		Flags_THEATER_3 = SceneFlags.EMPTY_FLAG;
		Flags_THEATER_4 = SceneFlags.EMPTY_FLAG;
		Flags_THEATER_PUZZLE = SceneFlags.EMPTY_FLAG;
		
		Flags_OBS_1 = SceneFlags.EMPTY_FLAG;
		Flags_OBS_2 = SceneFlags.EMPTY_FLAG;
		Flags_OBS_3 = SceneFlags.EMPTY_FLAG;
		Flags_OBS_4 = SceneFlags.EMPTY_FLAG;
		Flags_OBS_PUZZLE = SceneFlags.EMPTY_FLAG;
		
		Flags_SEWERS = SceneFlags.EMPTY_FLAG;
		Flags_TUTORIAL = SceneFlags.EMPTY_FLAG;

	}
	#endregion 
	private void Init()
	{
		// Init Dictionaries
		InitSceneNameDict();

		// Init 
		CurSceneName = SceneName.NO_SCENE;
		Flags_World = WorldFlags.EMPTY_FLAG;
	}	

#region SINGLETON_STUFF
	private static ProgressionManager _instance;
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
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;            
			DontDestroyOnLoad(this.gameObject); //Keep the instance going between scenes
			_instance.Init();
		}
		else
		{
			if (this != _instance)
			{
				DestroyImmediate(this.gameObject);
				return;
			}
		}
	}
#endregion
	
	
}