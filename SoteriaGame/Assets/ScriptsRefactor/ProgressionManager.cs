using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProgressionManager : MonoBehaviour
{
	private Dictionary< GameEnums.SceneName, string > 		dict_SceneToStr =	
		new Dictionary< GameEnums.SceneName, string >();
	private Dictionary< GameEnums.SceneName, GameEnums.SceneFlags >	dict_SceneToSFlag =
		new Dictionary< GameEnums.SceneName, GameEnums.SceneFlags >();

	private GameEnums.WorldFlags	gameState;

	// TEMPORARY - Did not know where to put init stuff for ProgMan
#region DICTIONARY_INIT
	private void InitSceneNameDict()
	{
		dict_SceneToStr.Add( GameEnums.SceneName.MAIN_MENU, "MainMenu" );
		dict_SceneToStr.Add( GameEnums.SceneName.CREDITS, "Credits" );
		dict_SceneToStr.Add( GameEnums.SceneName.HARBOR, "Harbor" );
		dict_SceneToStr.Add( GameEnums.SceneName.HARBOR_RESPAWN, "HarborRespawn" );
		
		dict_SceneToStr.Add( GameEnums.SceneName.HUB_1, "HUBPass1" );
		dict_SceneToStr.Add( GameEnums.SceneName.HUB_2, "HUBPass2" );
		dict_SceneToStr.Add( GameEnums.SceneName.HUB_3, "HUBPass3" );
		dict_SceneToStr.Add( GameEnums.SceneName.HUB_4, "HUBPass4" );
		
		dict_SceneToStr.Add( GameEnums.SceneName.MUSIC_1, "MusicPass1" );
		dict_SceneToStr.Add( GameEnums.SceneName.MUSIC_2, "MusicPass2" );
		dict_SceneToStr.Add( GameEnums.SceneName.MUSIC_3, "MusicPass3" );
		dict_SceneToStr.Add( GameEnums.SceneName.MUSIC_4, "MusicPass4" );
		dict_SceneToStr.Add( GameEnums.SceneName.MUSIC_PUZZLE, "MusicPuzzle" );
		
		dict_SceneToStr.Add( GameEnums.SceneName.THEATER_1, "TheaterPass1" );
		dict_SceneToStr.Add( GameEnums.SceneName.THEATER_2, "TheaterPass2" );
		dict_SceneToStr.Add( GameEnums.SceneName.THEATER_3, "TheaterPass3" );
		dict_SceneToStr.Add( GameEnums.SceneName.THEATER_4, "TheaterPass4" );
		dict_SceneToStr.Add( GameEnums.SceneName.THEATER_PUZZLE, "PuppetPuzzle" );
		
		dict_SceneToStr.Add( GameEnums.SceneName.OBS_1, "ObservatoryPass1" );
		dict_SceneToStr.Add( GameEnums.SceneName.OBS_2, "ObservatoryPass2" );
		dict_SceneToStr.Add( GameEnums.SceneName.OBS_3, "ObservatoryPass3" );
		dict_SceneToStr.Add( GameEnums.SceneName.OBS_4, "ObservatoryPass4" );
		dict_SceneToStr.Add( GameEnums.SceneName.OBS_PUZZLE, "ObservatoryPuzzle" );
		
		dict_SceneToStr.Add( GameEnums.SceneName.SEWERS, "Sewers" );
		dict_SceneToStr.Add( GameEnums.SceneName.TUTORIAL, "Tutorial" );
	}
	
	private void InitSceneInfoDict()
	{
		dict_SceneToSFlag.Add( GameEnums.SceneName.MAIN_MENU, GameEnums.SceneFlags.EMPTY_FLAG);
		dict_SceneToSFlag.Add( GameEnums.SceneName.CREDITS, GameEnums.SceneFlags.EMPTY_FLAG);
		dict_SceneToSFlag.Add( GameEnums.SceneName.HARBOR, GameEnums.SceneFlags.EMPTY_FLAG);
		dict_SceneToSFlag.Add( GameEnums.SceneName.HARBOR_RESPAWN,  GameEnums.SceneFlags.EMPTY_FLAG);
		
		dict_SceneToSFlag.Add( GameEnums.SceneName.HUB_1, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.HUB_2, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.HUB_3, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.HUB_4, GameEnums.SceneFlags.EMPTY_FLAG );
		
		dict_SceneToSFlag.Add( GameEnums.SceneName.MUSIC_1, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.MUSIC_2, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.MUSIC_3, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.MUSIC_4, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.MUSIC_PUZZLE, GameEnums.SceneFlags.EMPTY_FLAG );
		
		dict_SceneToSFlag.Add( GameEnums.SceneName.THEATER_1, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.THEATER_2, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.THEATER_3, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.THEATER_4, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.THEATER_PUZZLE, GameEnums.SceneFlags.EMPTY_FLAG );
		
		dict_SceneToSFlag.Add( GameEnums.SceneName.OBS_1, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.OBS_2, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.OBS_3, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.OBS_4, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.OBS_PUZZLE, GameEnums.SceneFlags.EMPTY_FLAG );
		
		dict_SceneToSFlag.Add( GameEnums.SceneName.SEWERS, GameEnums.SceneFlags.EMPTY_FLAG );
		dict_SceneToSFlag.Add( GameEnums.SceneName.TUTORIAL, GameEnums.SceneFlags.EMPTY_FLAG );
	}
#endregion 
	private void Init()
	{
		// Init Dictionaries
		InitSceneInfoDict();
		InitSceneNameDict();
	}

	// START SINGLETON STUFF //
	// END SINGLETON STUFF //


}