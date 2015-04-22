using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	
	protected static GameDirector _instance;
	
#region Managers

	private AudioManager _audioManager;
	private InputManager _inputManager;
	private TimerManager _timerManager;

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
		this._inputManager.Update();
		this._audioManager.Update();
		this._timerManager.Update();
	}
	private void InitializeManagers()
	{
		//This is problematic (AddComponent)-> it forces the script to be a component and uses the 
		// Update function automatically each frame, only solution not use MonoBehavior <-- not so simple
		this._timerManager = this.gameObject.GetComponent<TimerManager>();
		this._audioManager = this.gameObject.AddComponent<AudioManager>();
		this._audioManager.Initialize();
		this._inputManager = this.gameObject.AddComponent<InputManager>();
		this._inputManager.Initialize();
	}
}

