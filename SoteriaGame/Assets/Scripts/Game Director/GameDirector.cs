using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {

    private static GameDirector _instance;
    private GameObject _player = null;

    #region Managers

	private AudioManager     	_audioManager;
	private InputManager     	_inputManager;
	private TimerManager 		_timerManager;
	private HUDManager       	_HUDManager;
	private EncounterManager 	_encounterManager;
	private StateManager     	_stateManager;

    #endregion

    public static GameDirector instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameDirector>();
            return (GameDirector)(_instance);
        }
    }

	public void InitializePlayer()
	{
		if (_player == null) 
		{
			_player = GameObject.FindWithTag("Player");
		}
	}
    public GameObject GetPlayer()
    {
        return _player;
    }

    // Use this for initialization
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            this.InitializeManagers();
            DontDestroyOnLoad(this); //Keep the instance going between scenes
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
		this._stateManager.Update(); //No update happening we can remove it later on
		this._HUDManager.Update(); //No update happening we can remove it later on
		this._inputManager.Update();
		this._audioManager.Update(); //No update happening we can remove it later on
		this._timerManager.Update();
    }

    private void InitializeManagers()
    {
        //This is problematic (AddComponent)-> it forces the script to be a component and uses the 
        // Update function automatically each frame, only solution not use MonoBehavior <-- not so simple

		this._timerManager = this.gameObject.GetComponent<TimerManager>();
		this._audioManager = this.gameObject.AddComponent<AudioManager>();
		this._inputManager = this.gameObject.AddComponent<InputManager>();
		this._HUDManager = this.gameObject.AddComponent<HUDManager> ();
		this._encounterManager = this.gameObject.AddComponent<EncounterManager> ();
		this._stateManager = this.gameObject.AddComponent<StateManager>();
		
		this._timerManager.Initialize(); //->quick hack, needs to change later.
		this._audioManager.Initialize();
		this._inputManager.Initialize();
		this._HUDManager.Initialize();
		this._encounterManager.Initialize();
		this._stateManager.Initialize();

		this.InitializePlayer ();    
    }

	#region InputManager Methods
	
	public int GetQTECount()
	{
		return this._inputManager.GetPressCount ();
	}

	#endregion

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

    #region EncounterManager

	public EncounterState GetEncounterState()
	{
		return this._encounterManager.GetState();
	}

	public void StopEncounterMode()
	{
		_stateManager.ChangeGameState(GameStates.Normal);
		//Debug.Log ("Clearing black from enabling normal view");
		_HUDManager.EnableNormalView();
		GetPlayer().GetComponent<Player>().PlayerActionNormal();
		//this.gameObject.AddComponent<LevelManager>().SetActiveLevel("TestSceneWithArt");
	}

    public void StartEncounterMode()
    {
		if (_stateManager.GameState() != GameStates.Encounter) 
		{
			_stateManager.ChangeGameState (GameStates.Encounter);
		}

		_HUDManager.EnableEncounterView();
		GetPlayer().GetComponent<Player>().PlayerActionEncounter();
//		if (!lightCooldown)
//		{
//			_HUDManager.EnableEncounterView();
//			Debug.Log ("Black from encounter start");
//		}
//		else
//		{
//			Debug.Log ("Black from encounter start no pulse");
//			FadeToBlack();
//		}

	}

    public void TakeSafteyLight()
    {
		StopEncounterMode();
		/*Teleport to town center*/
    }

	public void LightReset()
	{
		if (this._stateManager.GameState () == GameStates.Encounter)
		{
			this._HUDManager.EnableEncounterView();
			Debug.Log ("Black from light reset");
		}
	}

	public void AbleToOvercome()
	{
		this._encounterManager.PlayerCanOvercome();
	}

//	public bool CanUseToken()
//	{
//		return this._encounterManager.CanUseToken ();
//	}

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
		this._player.GetComponent<Player>().BeginLingering();
	}

	public void ResetLinger()
	{
		this._player.GetComponent<Player>().ResetLinger();
		//Debug.Log ("Black from reseting linger");
		FadeToBlack();
		//ResetOverpower ();
	}

	public void PlayerOvercame()
	{
		this._encounterManager.PlayerOvercame();
		this._stateManager.ChangeGameState(GameStates.Normal);
	}

	public void FadeToBlack()
	{
		//Debug.Log ("Going black");
		this._HUDManager.FadeToBlack();
	}

	public void ClearFromBlack()
	{
		//Debug.Log ("Going clear");
		this._HUDManager.ClearFromBlack();
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

    #endregion

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
}

