using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	
	private static GameDirector _instance;
	private Player _player = null;
	
#region Managers

	private AudioManager _audioManager;
	private InputManager _inputManager;
	private TimerManager _timerManager;
	private HUDManager       _HUDManager;
	private EncounterManager _encounterManager;
	private StateManager     _stateManager;

#endregion
	
	public static GameDirector instance
	{
		get {
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<GameDirector>();
			return (GameDirector)(_instance);
		}
	}

	public Player getPlayer()
	{
		return _player;
	}

	// Use this for initialization
	private void Awake () 
	{
		if(_instance == null)
		{
			_instance = this;
			this.InitializeManagers();
			this.DontDestroyOnLoad(this); //Keep the instance going between scenes
		}
		else
		{
			if(this != _instance)
				this.Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	private void  Update () 
	{
		this._stateManager.Update(); //No update happening we can remove it later on
		this._HUDManager.Update(); //No update happening we can remove it later on
		this._inputManager.Update();
		this._audioManager.Update(); //No update happening we can remove it later on
		this._timerManager.Update();
		this._encounterManager.Update(); //No update happening we can remove it later on

	}
	private void InitializeManagers()
	{
		// This is problematic (AddComponent)-> it forces the script to be a component and uses the 
		// Update function automatically each frame, only solution not use MonoBehavior <-- not so simple
		//-----
		// solved this by setting enable to false in the awake function (this will stop  the start fuction from getting called)
		// setting update to public and calling update manually from the game director. Updates should be left to a minimum we'll
		// start using events soon.

		this._timerManager = this.gameObject.GetComponent<TimerManager>();
		this._audioManager = this.gameObject.AddComponent<AudioManager>();
		this._inputManager = this.gameObject.AddComponent<InputManager>();
		this._HUDManager = this.gameObject.AddComponent<HUDManager> ();
		this._encounterManager = this.gameObject.AddComponent<EncounterManager> ();
		this._stateManager = this.gameObject.AddComponent<StateManager>();

		this._timerManager.Initialize(); // quick hack -> this should work automatically weird....
		this._audioManager.Initialize();
		this._inputManager.Initialize();
		this._HUDManager.Initialize();
		this._encounterManager.Initialize();
		this._stateManager.Initialize();
	}



	/// <summary>
	/// add these in the encounter manager later on!
	/// After this build we'll have to start using events base on
	/// the what's happening in the game.
	/// </summary>
	/// 

	public void UpdateEncounterState(bool inStatus)
	{
		if (inStatus)
		{
			this._stateManager.ChangeGameState(GameStates.Encounter);
			this._HUDManager.EnableEncounterView();

		}
		else
		{
			this._stateManager.ChangeGameState(GameStates.Normal);
			this._HUDManager.EnableNormalView();
		}

	}
	/// <summary>
	/// Do we want to stop the ecounter with the safety light or just
	/// Change the sate so that the player is in "escape" mode or
	/// something like that. We'll discuss this later on.
	/// </summary>
	public void TakeSafteyLight()
	{
		this._encounterManager.DeActivateEncounter();
		Debug.Log("Exiting Encounter Mode");
	}
}

