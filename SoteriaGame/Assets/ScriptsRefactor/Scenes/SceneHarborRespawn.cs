using UnityEngine;
using System.Collections;

public class SceneHarborRespawn : Scene 
{
	const SceneFlags FailedObsDialogueExhausted			= SceneFlags.HEX_00000001;
	const SceneFlags FailedTheaterDialogueExhausted  	= SceneFlags.HEX_00000010;
	const SceneFlags FailedMusicDialogueExhausted		= SceneFlags.HEX_00000100;
	
	private GameObject _oMalleyFailedMusic;
	public Transform oMalleyFailedMusicSpawnLoc;

	private GameObject _oMalleyFailedTheater;
	public Transform oMalleyFailedTheaterSpawnLoc;

	private GameObject _oMalleyFailedObs;
	public Transform oMalleyFailedObsSpawnLoc;

	void Awake()
	{
		this.sceneName = SceneName.HARBOR_RESPAWN;

		// DIALOGUE SETUP //	
		_oMalleyFailedMusic = GameObject.Find ("O'MalleyFailedMusic");
		_oMalleyFailedMusic.transform.position = oMalleyFailedMusicSpawnLoc.position;
		_oMalleyFailedMusic.SetActive( false );
		
		_oMalleyFailedTheater = GameObject.Find ("O'MalleyFailedTheater");
		_oMalleyFailedTheater.transform.position = oMalleyFailedTheaterSpawnLoc.position;
		_oMalleyFailedTheater.SetActive( false );

		_oMalleyFailedObs = GameObject.Find ("O'MalleyFailedObservatory");
		_oMalleyFailedObs.transform.position = oMalleyFailedObsSpawnLoc.position;
		_oMalleyFailedObs.SetActive( false );
	}


	void Start()
	{
		if(/*FlagTools.World_CheckFlag( ProgressionManager.instance.Flags_World, WorldFlags.HUB_PHASE2 &&*/ 
		   !FlagTools.Scene_CheckFlag( ProgressionManager.instance.Flags_HARBOR_RESPAWN, FailedMusicDialogueExhausted ) )
		{
			// Activate Music Omalley+Dialogue
			_oMalleyFailedMusic.SetActive( true );

			// BELOW SHOULD BE PLACED IN SCRIPT WHERE ACTUAL CONVERSATION TAKES PLACE
			ProgressionManager.ProgressActions.DialogueActs.FailedMusicDialogueExhausted();
		}
		else if( /*FlagTools.World_CheckFlag( ProgressionManager.instance.Flags_World, WorldFlags.HUB_PHASE3 &&*/ 
		        !FlagTools.Scene_CheckFlag( ProgressionManager.instance.Flags_HARBOR_RESPAWN, FailedTheaterDialogueExhausted ) )
		{
			// Activate Theater Omalley+Dialogue
			_oMalleyFailedTheater.SetActive( true );

			// BELOW SHOULD BE PLACED IN SCRIPT WHERE ACTUAL CONVERSATION TAKES PLACE
			ProgressionManager.ProgressActions.DialogueActs.FailedTheaterDialogueExhausted();
		}
		else if(/*FlagTools.World_CheckFlag( ProgressionManager.instance.Flags_World, WorldFlags.HUB_PHASE4 &&*/  
		        !FlagTools.Scene_CheckFlag( ProgressionManager.instance.Flags_HARBOR_RESPAWN, FailedObsDialogueExhausted ) )
		{
			// Activate Theater Omalley+Dialogue
			_oMalleyFailedObs.SetActive( true );
			
			// BELOW SHOULD BE PLACED IN SCRIPT WHERE ACTUAL CONVERSATION TAKES PLACE
			ProgressionManager.ProgressActions.DialogueActs.FailedObsDialogueExhausted();
		}
	}
}
