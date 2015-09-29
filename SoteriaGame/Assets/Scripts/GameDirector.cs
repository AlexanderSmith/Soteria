using UnityEngine;
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
	private int _gamePhase = 1;
	private GameObject UIObject;

    #region Managers

	private AudioManager     	_audioManager;
	private TimerManager 		_timerManager;
	private HudManager       	_HUDManager;
	private EncounterManager 	_encounterManager;
	private StateManager     	_stateManager;
	private DialogueManager 	_dialoguemanager;
	private LevelManager		_levelmanager;

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
		this._levelmanager = this.gameObject.GetComponent<LevelManager>();

		this.InitializePlayer ();  
		
		this._audioManager.Initialize();
		this._HUDManager.Initialize();
		this._encounterManager.Initialize();
		this._stateManager.Initialize();
		this._dialoguemanager.Initialize ();
		this._levelmanager.Initialize ();
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

	public int GetGamePhase()
	{
		return this._gamePhase;
	}


    // Update is called once per frame
    private void Update()
    {
		//Dialogue
		//this._dialoguemanager.Update();
	}
	///////////////////////////////////////////////////////////////////
	////////////////// LEVEL MANAGER FUNCTIONS ////////////////////////
	///////////////////////////////////////////////////////////////////

	public void LoadLevel(int level)
	{
		this._levelmanager.LoadLevel (level);
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
		if (_stateManager.GameState() != GameStates.Encounter) 
		{
			_stateManager.ChangeGameState (GameStates.Encounter);
		}
		
		_HUDManager.EnableEncounterView();
		this.SetClearStatus(false);
		this._player.PlayerActionEncounter();
	}

	public void StopEncounterMode()
	{
		_stateManager.ChangeGameState(GameStates.Normal);
		_HUDManager.DisableEncounterView();
		this._player.PlayerActionNormal();
	}

	public void TakeSafteyLight()
	{
		StopEncounterMode();
		/*Teleport to town center*/
		this._levelmanager.LoadLevel (1);// ("FullModelHub");
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

