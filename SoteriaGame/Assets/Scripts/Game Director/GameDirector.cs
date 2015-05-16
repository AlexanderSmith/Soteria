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
			_player.GetComponent<EncounterMovementController>().Initialize(_stateManager);
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
		return this._inputManager.getPressCount ();
	}

	public bool GetBool()
	{
		return true;
	}
	
	#endregion


    #region EncounterManager

    public void StopEncounterMode()
    {
        _stateManager.ChangeGameState(GameStates.Normal);
        _HUDManager.EnableNormalView();
		_player.GetComponent<EncounterMovementController> ().OverCome ();
		_encounterManager.KillSafetyLight();
        //this.gameObject.AddComponent<LevelManager>().SetActiveLevel("TestSceneWithArt");
    }


    public void StartEncounterMode(bool lightCooldown)
    {
		if (_stateManager.GameState() != GameStates.Encounter) 
		{
			_stateManager.ChangeGameState (GameStates.Encounter);
		}
		if (!lightCooldown)
		{
			_HUDManager.EnableEncounterView();
		}
	}

    public void TakeSafteyLight()
    {
        //StopEncounterMode();
		_stateManager.ChangeGameState (GameStates.InLight);
        //Debug.Log("Switching from encounter to safety light mode");
		_encounterManager.InitializeSafetyLight();
    }

	public void LightReset()
	{
		if (_stateManager.GameState () == GameStates.Encounter)
		{
			_HUDManager.EnableEncounterView();
		}
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

