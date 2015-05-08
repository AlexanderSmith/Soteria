using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {

//	private Player _player;

	protected static GameDirector _instance;
	
#region Managers

	private AudioManager     _audioManager;
	private InputManager     _inputManager;
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

	// Use this for initialization
	private void Awake () 
	{
		if(_instance == null)
		{
			_instance = this;
			this.InitializeManagers();
			DontDestroyOnLoad(this); //Keep the instance going between scenes
		}
		else
		{
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	private void  Update () {
	
		_inputManager._Update();
		_audioManager._Update(); //This will change, We'll use events rather than Updates.

	}
	private void InitializeManagers()
	{
		//This is problematic (AddComponent)-> it forces the script to be a component and uses the 
		// Update function automatically each frame, only solution not use MonoBehavior <-- not so simple

		_audioManager = this.gameObject.AddComponent<AudioManager>();
		_audioManager.Initialize();
		_inputManager = this.gameObject.AddComponent<InputManager>();
		_inputManager.Initialize();
		_HUDManager = this.gameObject.AddComponent<HUDManager> ();
		_HUDManager.Initialize();
		_encounterManager = this.gameObject.AddComponent<EncounterManager> ();
		_encounterManager.Initialize();
        _stateManager = this.gameObject.AddComponent<StateManager>();
        _stateManager.Initialize();
    }

#region EncounterManager

    public void StopEncounterMode()
    {
        _stateManager.gameState = StateManager.GameStates.Normal;
        _HUDManager.EnableNormalView();
        _encounterManager.enabled = false;
    }


    public void StartEncounterMode()
    {
        _stateManager.gameState = StateManager.GameStates.Encounter;
        _HUDManager.EnableEncounterView();
        _encounterManager.enabled = true;
    }

    public void TakeSafteyLight()
    {
        StopEncounterMode();
        Debug.Log("Exiting Encounter Mode");
    }
#endregion
}

