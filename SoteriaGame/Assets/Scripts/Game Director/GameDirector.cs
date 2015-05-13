using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {

    protected static GameDirector _instance;
    private GameObject _player = null;

    #region Managers

    private AudioManager _audioManager;
    private InputManager _inputManager;
    private HUDManager _HUDManager;
    private EncounterManager _encounterManager;
    private StateManager _stateManager;

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

    public GameObject GetPlayer()
    {
		if (_player == null) 
		{
			_player = GameObject.Find("Player");
			_player.GetComponent<EncounterMovementController>().Initialize(_stateManager);
		}
        return _player;
    }

    // Use this for initialization
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            this.InitializeManagers();
			this.GetPlayer();
            DontDestroyOnLoad(this); //Keep the instance going between scenes
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

	private void OnLevelWasLoaded()
	{
		InitializeManagers ();
	}

    // Update is called once per frame
    private void Update()
    {

    }
    private void InitializeManagers()
    {
        //This is problematic (AddComponent)-> it forces the script to be a component and uses the 
        // Update function automatically each frame, only solution not use MonoBehavior <-- not so simple

        _stateManager = this.gameObject.AddComponent<StateManager>();
        _stateManager.Initialize();
        _audioManager = this.gameObject.AddComponent<AudioManager>();
        _audioManager.Initialize();
        //_inputManager = this.gameObject.AddComponent<InputManager>();
        //_inputManager.Initialize();
        _HUDManager = this.gameObject.AddComponent<HUDManager>();
        _HUDManager.Initialize();
        _encounterManager = this.gameObject.AddComponent<EncounterManager>();
        _encounterManager.Initialize();
        
    }

    #region EncounterManager

    public void StopEncounterMode()
    {
        _stateManager.ChangeGameState(GameStates.Normal);
        _HUDManager.EnableNormalView();
		_player.GetComponent<EncounterMovementController> ().OverCome ();
        //this.gameObject.AddComponent<LevelManager>().SetActiveLevel("TestSceneWithArt");
    }


    public void StartEncounterMode()
    {
        _stateManager.ChangeGameState(GameStates.Encounter);
        _HUDManager.EnableEncounterView();
    }

    public void TakeSafteyLight()
    {
        StopEncounterMode();
		_stateManager.ChangeGameState (GameStates.Hidden);
        //Debug.Log("Exiting Encounter Mode");
		_encounterManager.InitializeSafetyLight();
    }
    #endregion
}

