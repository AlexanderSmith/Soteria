﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DialogueManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(HudManager))]
[RequireComponent(typeof(EncounterManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(LevelManager))]

public class GameDirector : MonoBehaviour {

    private static GameDirector _instance;
    private Player _player = null;
	private GameObject UIObject;

	// Game progression variables
	private int _gamePhase;
	private bool token;
	private bool lantern;
	private bool compass;

    #region Managers

	private AudioManager     	_audioManager;
	private TimerManager 		_timerManager;
	private HudManager       	_HUDManager;
	private EncounterManager 	_encounterManager;
	private StateManager     	_stateManager;
	private DialogueManager 	_dialoguemanager;
	private LevelManager		_levelManager;

    #endregion

	///////////////////////////////////////////////////////////////////
	////////////////////////// INITIALIZATION /////////////////////////
	///////////////////////////////////////////////////////////////////
	
    public static GameDirector instance
    {
        get
        {
            if (_instance == null)
			{
                _instance = GameObject.FindObjectOfType<GameDirector>();
			}
            return _instance;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;            
            DontDestroyOnLoad(this.gameObject); //Keep the instance going between scenes
			this.InitializeManagers();
        }
        else
        {
            if (this != _instance)
			{
                DestroyImmediate(this.gameObject);
				return;
			}
        }

		if (this._gamePhase < 1)
		{
			this.StartGame();
		}
    }

	void StartGame()
	{
		this.token = false;
		this.lantern = false;
		this.compass = false;
	}
	
	private void InitializeManagers()
    {
        //This is problematic (AddComponent)-> it forces the script to be a component and uses the 
        // Update function automatically each frame, only solution not use MonoBehavior <-- not so simple
		
		this._audioManager = this.gameObject.GetComponent<AudioManager>();
		this._HUDManager = this.gameObject.GetComponent<HudManager> ();
		this._encounterManager = this.gameObject.GetComponent<EncounterManager> ();
		this._stateManager = this.gameObject.GetComponent<StateManager>();
		this._dialoguemanager = this.gameObject.GetComponent<DialogueManager> ();
		this._levelManager = this.gameObject.GetComponent<LevelManager> ();

		//this.InitializePlayer ();  
		
		this._audioManager.Initialize();
		this._HUDManager.Initialize();
		this._encounterManager.Initialize();
		this._stateManager.Initialize();
		this._dialoguemanager.Initialize (); 
		this._levelManager.Initialize ();
    }
	
	public void InitializePlayer()
	{
		if (_player == null) 
		{
			_player = GameObject.FindWithTag("Player").GetComponent<Player>();
		}
	}
    public Player GetPlayer()
    {
        return _player;
    }

    // Update is called once per frame
    private void Update()
    {
		//Dialogue
		//this._dialoguemanager.Update();
	}

	///////////////////////////////////////////////////////////////////
	////////////////// GAME PROGRESSION FUNCTIONS /////////////////////
	///////////////////////////////////////////////////////////////////
	
	#region Game Progression
	
	public int GetGamePhase()
	{
		return this._gamePhase;
	}
	
	public void TokenTrue()
	{
		this.token = true;
		this._HUDManager.TokenTrue(token);
	}
	
	public bool GetToken()
	{
		return this.token;
	}
	
	public void LanternTrue()
	{
		this.lantern = true;
		this._HUDManager.LanternTrue(lantern);
	}
	
	public bool GetLantern()
	{
		return this.lantern;
	}
	
	#endregion

	///////////////////////////////////////////////////////////////////
	////////////////// LEVEL MANAGER FUNCTIONS ////////////////////////
	///////////////////////////////////////////////////////////////////

	public void LoadLevel(int level)
	{
		this._levelManager.LoadLevel(level);
	}

	///////////////////////////////////////////////////////////////////
	////////////////// STATE MANAGER FUNCTIONS ////////////////////////
	///////////////////////////////////////////////////////////////////

	#region StateManager

	public GameStates GetGameState()
	{
		return this._stateManager.GameState();
	}

	public void ChangeGameState(GameStates state)
	{
		this._stateManager.ChangeGameState (state);
	}

	#endregion

	///////////////////////////////////////////////////////////////////
	///////////////////// HUD MANAGER FUNCTIONS ///////////////////////
	///////////////////////////////////////////////////////////////////

//	public void HUDSceneStart()
//	{
//		this._HUDManager.HUDSceneStart(token, lantern);
//	}

	public void SetClearStatus (bool inStatus)
	{
		_HUDManager.isToClear = inStatus;	
	}
	
	///////////////////////////////////////////////////////////////////
	////////////////// ENCOUNTER MANAGER FUNCTIONS ////////////////////
	///////////////////////////////////////////////////////////////////

    #region EncounterManager

	public EncounterState GetEncounterState()
	{
		return this._encounterManager.GetState();
	}

	public void Encounter(GameObject enemy)
	{
		this._encounterManager.Encounter(enemy);
	}

	public void StartEncounterMode()
	{
		if (this._stateManager.GameState() != GameStates.Encounter) 
		{
			this._stateManager.ChangeGameState (GameStates.Encounter);
		}
		
		this._HUDManager.EnableEncounterView();
		this.SetClearStatus(false);
		this._player.PlayerActionEncounter();
	}

	public void StopEncounterMode()
	{
		this._stateManager.ChangeGameState(GameStates.Normal);
		this._HUDManager.DisableEncounterView();
		this._player.PlayerActionNormal();
	}

	public void TakeSafteyLight()
	{
		this.FindEnemies();
		//this._levelManager.LoadLevel(1);
		this._encounterManager.TokenUsed();
		this.StopEncounterMode();
		/*Teleport to town center*/
		Application.LoadLevel("FullModelHub");
		//this._levelManager.LoadLevel(1); //("FullModelHub")
	}

	public void FindEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			//enemy.SetActive(false);
			Destroy(enemy);
		}
	}

	public void KillEnemy()
	{
		this._encounterManager.DestroyMe();
	}

	public void AbleToOvercome()
	{
		this._encounterManager.PlayerCanOvercome();
	}

	public bool GetOvercomeBool()
	{
		return this._encounterManager.GetOvercomeStatus();
	}
	
	public void TryingToOvercome()
	{
		this._encounterManager.AddToOvercomeCounter();
	}

	public void FailedToLinger()
	{
		this._encounterManager.SubtractFromOvercomeCounter();
		ResetLinger();
	}

	public void BeginLingering()
	{
		this._player.BeginLingering();
	}

	public void ResetLinger()
	{
		this._player.ResetLinger();
		this.SetClearStatus (false);
	}

	public void PlayerOvercame()
	{
		//GetPlayer().GetComponent<Player>().Overcome();
		this._encounterManager.PlayerOvercame();
		this._stateManager.ChangeGameState(GameStates.Normal);
	}

	public void ResetGameOverTimer()
	{
		this._encounterManager.ResetGameOverTimer();
	}

	public void Overpower()
	{
		this._encounterManager.Overpower ();
	}

	public void ResetOverpower()
	{
		this._encounterManager.ResetOverpower ();
	}

	public void NextOPStage()
	{
		this._encounterManager.NextOPStage ();
	}

	public void UseLantern()
	{
		this._encounterManager.LanternUsed();
	}

	public void PlayerOnObservatoryTile()
	{
		this._encounterManager.TileTimer();
	}

    #endregion

	///////////////////////////////////////////////////////////////////
	////////////////// AUDIO MANAGER FUNCTIONS ////////////////////////
	///////////////////////////////////////////////////////////////////

	#region AudioManager Methods
	public void PlayAudioClip(AudioID inAID)
	{
		this._audioManager.PlayAudio(inAID);
	}
	/// <summary>
	/// Adds the audio clip Programmatically.
	/// </summary>
	public void AddAudioClip(string inClipName, AudioID inAID, GameObject inGameObj = null)
	{
		this._audioManager.AddAudioSource (inClipName, inAID, inGameObj);
	}
	
	/// <summary>
	/// Attachs the audio source that was added to another object from the inspector 
	/// so that the manager can control it.
	/// </summary>
	public void AttachAudioSource( AudioSource inAudioSrc,GameObject inGameObj, string inName)
	{
		this._audioManager.AttachAudioSource (inAudioSrc,inGameObj,inName);
	}
	///Other Stuff to Add.
	/// --->
	//set Parameters Methods
	//Remove Audio Clip
	//Silence Audio Clip
	//Clone Audio Clip
	//Is Done playing Clip
	//Queue Clips
	
	#endregion
	
	///////////////////////////////////////////////////////////////////
	////////////////// DIALOGUE MANAGER FUNCTIONS /////////////////////
	///////////////////////////////////////////////////////////////////
	
	#region DialogueManager Methods

	public GameObject getDialogueInterface()
	{
		return this._dialoguemanager.getDialogueInterface();
	}
	
	public bool isDialogueActive()
	{
		return this._dialoguemanager.isCurrentlyActive();
	}
	public void StartDialogue(GameObject NPC, GameObject Player)
	{
		this._dialoguemanager.startdialogue(NPC,Player);
	}

	#endregion

}

