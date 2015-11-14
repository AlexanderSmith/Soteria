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
	private GameObject UIObject;

	// Game progression variables
	private int _gamePhase;
	private bool token;
	private bool lantern;
	private bool compass;
	private LanternController _lanternController;

	private int _hubPhase;
	private bool _tokenUsed;
	private bool _canFight;

	// Puzzle activation variables
	private bool _musicPuzzleActivated;
	private bool _puppetPuzzleActivated;
	private bool _observatoryPuzzleActivated;

	// District visitation bools for objective
	private bool _visitedSewer;
	private bool _musicPass1;
	private bool _theaterPass1;
	private bool _obsPass1;

	// Cards collected
	private string _musicDistrictCard;
	private bool _musicDistrictHaveCard;
	private string _theaterDistrictCard;
	private bool _theaterDistrictHaveCard;
	private string _observatoryDistrictCard;
	private bool _observatoryDistrictHaveCard;
	private int _cardsHeld;

	public float flashBangLife = 3.0f;
	public Vector3 flashBangHeight = new Vector3(0, 6.0f, 0);
	public float flashBangDistance = 3.0f;

	private ParticleSystem beam;
	public float beamLife = 3.0f;
	public Vector3 beamHeight = new Vector3 (0, 35.0f, 0);

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
		this._gamePhase++;
		this._hubPhase++;
		this.token = false;
		this.lantern = false;
		this.compass = false;
		this._tokenUsed = false;
		this._canFight = false;
		this._visitedSewer = false;
		this._musicPass1 = false;
		this._theaterPass1 = false;
		this._obsPass1 = false;
		this._musicPuzzleActivated = false;
		this._puppetPuzzleActivated = false;
		this._observatoryPuzzleActivated = false;
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
		if (beam != null)
		{
			beam.transform.position = this.GetPlayer().gameObject.transform.position + beamHeight;
		}
	}

	///////////////////////////////////////////////////////////////////
	////////////////// GAME PROGRESSION FUNCTIONS /////////////////////
	///////////////////////////////////////////////////////////////////
	
	#region Game Progression
	
	public int GetGamePhase()
	{
		return this._gamePhase;
	}

	public void AddGamePhase()
	{
		this._gamePhase++;
	}
	
	public void TokenTrue()
	{
		this.token = true;
		this._audioManager.PlayAudio(AudioID.TokenUse);
		this._HUDManager.TokenTrue(this.token);
	}
	
	public bool GetToken()
	{
		return this.token;
	}

	public void CompassTrue()
	{
		this.compass = true;
		this._HUDManager.CompassTrue(this.compass);
	}

	public bool GetCompass()
	{
		return this.compass;
	}
	
	public void LanternTrue()
	{
		this.lantern = true;
		this._HUDManager.LanternTrue(this.lantern);
	}
	
	public bool GetLantern()
	{
		return this.lantern;
	}

	public int GetHubPhase()
	{
		return this._hubPhase;
	}

	public void HubPhase2()
	{
		this._hubPhase = 2;
	}

	public void HubPhase3()
	{
		this._hubPhase = 3;
	}

	public void HubPhase4()
	{
		this._hubPhase = 4;
	}

	public void HubPhase5()
	{
		this._hubPhase = 5;
	}

	public bool CanFight()
	{
		return this._canFight;
	}

	// When suit removed, player now able to fight shadow creatures -- will need more than this bool flip
	public void SuitRemoved()
	{
		this._canFight = true;
	}

	public void VisitedSewer()
	{
		this._visitedSewer = true;
	}

	public void ResetSewer()
	{
		this._visitedSewer = false;
	}

	public bool GetVisitedSewer()
	{
		return this._visitedSewer;
	}

	public void MusicPass1Done()
	{
		this.ResetSewer();
		this._musicPass1 = true;
	}

	public bool GetMusicPass1()
	{
		return this._musicPass1;
	}

	public void TheaterPass1Done()
	{
		this.ResetSewer();
		this._theaterPass1 = true;
	}

	public bool GetTheaterPass1()
	{
		return this._theaterPass1;
	}

	public void ObservatoryPass1Done()
	{
		this.ResetSewer();
		this._obsPass1 = true;
	}

	public bool GetObservatoryPass1()
	{
		return this._obsPass1;
	}

	public void MusicCardCollected(string inCard)
	{
		this._musicDistrictCard = inCard;
	}

	public void TheaterCardCollected(string inCard)
	{
		this._theaterDistrictCard = inCard;
	}

	public void ObservatoryCardCollected(string inCard)
	{
		this._observatoryDistrictCard = inCard;
	}

	public bool CheckCards(string inMusic, string inTheater, string inObservatory)
	{
		return this._musicDistrictCard == inMusic &&
			this._theaterDistrictCard == inTheater &&
			this._observatoryDistrictCard == inObservatory;
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

	public void EncounterClear()
	{
		this._HUDManager.EncounterClear();
	}

	public void ChangeObjective(GameObject gObj)
	{
		this._HUDManager.ChangeObjective(gObj);
	}

	public void StartCardInteraction(Sprite inSprite, int inDist, GameObject inCardObj)
	{
		this._HUDManager.StartCardInteraction(inSprite, inDist, inCardObj);
	}

	public void EndCardInteraction(bool inResponse)
	{
		this._HUDManager.EndCardInteraction(inResponse);
	}

	public void PlayerHasCard(int inDist, string inCard)
	{
		switch (inDist)
		{
		case 1:
		case 2:
		case 3:
			if (!this._musicDistrictHaveCard)
			{
				this._musicDistrictHaveCard = true;
				this._musicDistrictCard = inCard;
			}
			else
			{
				this._musicDistrictCard = inCard;
			}
			break;
		case 4:
		case 5:
		case 6:
			if (!this._theaterDistrictHaveCard)
			{
				this._theaterDistrictHaveCard = true;
				this._theaterDistrictCard = inCard;
			}
			else
			{
				this._theaterDistrictCard = inCard;
			}
			break;
		case 7:
		case 8:
		case 9:
			if (!this._observatoryDistrictHaveCard)
			{
				this._observatoryDistrictHaveCard = true;
				this._observatoryDistrictCard = inCard;
			}
			else
			{
				this._observatoryDistrictCard = inCard;
			}
			break;
		};

		this._cardsHeld = 0;
		if (this._musicDistrictHaveCard)
		{
			this._cardsHeld++;
		}
		if (this._theaterDistrictHaveCard)
		{
			this._cardsHeld++;
		}
		if (this._observatoryDistrictHaveCard)
		{
			this._cardsHeld++;
		}
		this._HUDManager.DisplayCards(this._cardsHeld);
	}

	public string GetMusicDistrictCard()
	{
		if (this._musicDistrictHaveCard)
		{
			return this._musicDistrictCard;
		}
		else
		{
			return null;
		}
	}

	public string GetTheaterDistrictCard()
	{
		if (this._theaterDistrictHaveCard)
		{
			return this._theaterDistrictCard;
		}
		else
		{
			return null;
		}
	}

	public string GetObservatoryDistrictCard()
	{
		if (this._observatoryDistrictHaveCard)
		{
			return this._observatoryDistrictCard;
		}
		else
		{
			return null;
		}
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

//	public void MusicPuzzleEncounter(GameObject enemy)
//	{
//		this._encounterManager.MusicPuzzleEncounter(enemy);
//	}

	public void StartEncounterMode()
	{
		if (this._stateManager.GameState() != GameStates.Encounter) 
		{
			this._stateManager.ChangeGameState (GameStates.Encounter);
		}
		
		this._HUDManager.EnableEncounterView();
		this.SetClearStatus(false);
		// Take away player's ability to fight shadow creatures until after getting rid of the suit pieces
		if (this._canFight)
		{
			this._player.PlayerActionEncounter();
		}
		else
		{
			this._player.PlayerActionNoFighting();
		}
	}

	public void StopEncounterMode()
	{
		this._stateManager.ChangeGameState(GameStates.Normal);
		this._HUDManager.DisableEncounterView();
		this._player.PlayerActionNormal();
	}

//	public void StartMusicPuzzleEncounter()
//	{
//		if (this._stateManager.GameState() != GameStates.Encounter) 
//		{
//			this._stateManager.ChangeGameState (GameStates.Encounter);
//		}
//		this._HUDManager.EnableEncounterView();
//		this.SetClearStatus(false);
//		if (this._canFight)
//		{
//			this._player.PlayerActionMusicPuzzle();
//		}
//		else
//		{
//			this._player.PlayerActionNoFighting();
//		}
//	}

	public void TakeSafteyLight()
	{
		if (!this._tokenUsed)
		{
			this._audioManager.PlayAudio(AudioID.TokenUse);
			this._tokenUsed = true;
			// Beam
			GameObject soteriaBeam = Instantiate(Resources.Load("ParticleEffects/Beam")) as GameObject;
			beam = soteriaBeam.GetComponent<ParticleSystem>();
			beam.transform.position = this.GetPlayer().gameObject.transform.position + beamHeight;
			beam.Play();

			string level;

			/*Teleport to town center*/
			switch (_hubPhase)
			{
			case 5:
				level = "HUBPass3";
				StartCoroutine(BeamLevel(level, beamLife));
				break;
			case 1:
				level = "HUBPass1";
				StartCoroutine(BeamLevel(level, beamLife));
				break;
			case 2:
				level = "HUBPass2";
				StartCoroutine(BeamLevel(level, beamLife));
				break;
			case 3:
				level = "HUBPass3";
				StartCoroutine(BeamLevel(level, beamLife));
				break;
			case 4:
				level = "HUBPass4";
				StartCoroutine(BeamLevel(level, beamLife));
				break;
			}

			Destroy(soteriaBeam, beamLife);
		}
	}

	IEnumerator BeamLevel(string inLevel, float inBeamLife)
	{
		yield return new WaitForSeconds (inBeamLife);
		this.FindEnemies();
		this._encounterManager.TokenUsed();
		if (this._lanternController != null)
		{
			this.RechargeLantern();
		}
		this.ClearAudioList();
		Application.LoadLevel(inLevel);
		this._tokenUsed = false;
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
		GameObject flashBang = Instantiate(Resources.Load("ParticleEffects/FlashBang")) as GameObject;
		ParticleSystem bang = flashBang.GetComponent<ParticleSystem>();
		bang.transform.position = this.GetPlayer().gameObject.transform.position + (flashBangDistance * this.GetPlayer().transform.forward.normalized) + flashBangHeight;
		bang.Play();
		Destroy(flashBang, flashBangLife);
	}

	public void PlayerOnObservatoryTile()
	{
		this._encounterManager.TileTimer();
	}

	public void InitializeLanternController(LanternController lantCont)
	{
		this._lanternController = lantCont;
	}

	void RechargeLantern()
	{
		this._lanternController.RechargeLantern();
	}

	public void CheckLantern()
	{
		if (this._lanternController != null)
		{
			this.RechargeLantern();
		}
	}

	public void EncounterOver()
	{
		// reset encounter for game over
		this._encounterManager.StopEncounterFromToken();
		this.GameOver();
	}

	public void GameOver()
	{
		this.FindEnemies();
		this.StopEncounterMode();
		if (this._lanternController != null)
		{
			this.RechargeLantern();
		}
		this.ClearAudioList();
		Application.LoadLevel("HarborRespawn");
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

	public void StopAudioClip(AudioID inAID)
	{
		this._audioManager.StopAudio(inAID);
	}

	public void ClearAudioList()
	{
		this._audioManager.ClearAudioList();
	}

	public void ChangeVolume(AudioID inAID, float inVolume)
	{
		this._audioManager.ChangeVolume(inAID, inVolume);
	}
	
	public void AddVolume(AudioID inAID, float inVolume)
	{
		this._audioManager.AddVolume(inAID, inVolume);
	}
	
	public void SubtractVolume(AudioID inAID, float inVolume)
	{
		this._audioManager.SubtractVolume(inAID, inVolume);
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

	public bool isClipPlaying(AudioID inAID)
	{
		return this._audioManager.isClipPlaying(inAID);
	}
	
	public void CollectAudioClipsForDialogue(string inFolderName, string inAID)
	{
		AudioID aid = this._audioManager.getIDByName(inAID);
		this._audioManager.CollectDialogueAudioClips(inFolderName, aid);
	}
	
	public void CollectAudioClips(string inFolderName, string inAID)
	{
		AudioID aid = this._audioManager.getIDByName(inAID);
		this._audioManager.CollectAudioClips(inFolderName, aid);
	}

	///Other Stuff to Add.
	/// --->
	//set Parameters Methods
	//Remove Audio Clip
	//Silence Audio Clip
	//Clone Audio Clip
	//Is Done playing Clip
	//Queue Clips

	public float GetPuzzleWinVolume()
	{
		return this._audioManager.GetPuzzleWinVolume();
	}
	
	public float GetVolume(AudioID inAID)
	{
		return this._audioManager.GetVolume(inAID);
	}
	
	#endregion
	
	///////////////////////////////////////////////////////////////////
	////////////////// DIALOGUE MANAGER FUNCTIONS /////////////////////
	///////////////////////////////////////////////////////////////////
	
	#region DialogueManager Methods

	public GameObject getDialogueInterface()
	{
		return null;
	}
	
	public bool isDialogueActive()
	{
		return this._dialoguemanager.isDialogueActive();
	}

	public void SetupDialogue(string txtname, AudioID inAID)
	{
		this._dialoguemanager.ReloadDialogueData(txtname, inAID);
	}

	public void StartDialogue()
	{
		this._dialoguemanager.StartDialogue();
	}

	public void EndDialogue()
	{
		this._dialoguemanager.EndDialogue();
	}

	public void SkipLine()
	{
		this._dialoguemanager.SkipLine();
	}

	#endregion
}