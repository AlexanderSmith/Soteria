using UnityEngine;
using System.Collections;

public enum StatueCrumbleState
{
	WHOLE = 0,
	CRUMBLEONE = 1,
	CRUMBLETWO = 2,
	CRUMBLETHREE = 3,
	CRUMBLED = 4
}

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
	private bool _dreams;
	private LanternController _lanternController;

	private int _hubPhase;
	private bool _tokenUsed;
	private bool _canFight;

	// Zone transition bools
	private bool _fromPuzzle;
	private bool _fromMusicDistrict;
	private bool _fromTheaterDistrict;
	private bool _fromObservatoryDistrict;

	// Dialogue heard bools
	private bool _obsIntro;
	private bool _theaterIntro;

	// First time entering puzzle bools for O'Malley interactions
	private bool _musicFirstTime;
	private bool _theaterFirstTime;
	private bool _observatoryFirstTime;

	// Puzzle activation variables
	private bool _musicPuzzleActivated;
	private bool _puppetPuzzleActivated;
	private bool _observatoryPuzzleActivated;

	// Pass 1 district visitation bools for objective
	private bool _visitedSewer;
	private bool _musicPass1;
	private bool _theaterPass1;
	private bool _obsPass1;

	// Cards collected
	private bool _firstTailorInteraction;
	private string _musicDistrictCard;
	private bool _musicDistrictHaveCard;
	private string _theaterDistrictCard;
	private bool _theaterDistrictHaveCard;
	private string _observatoryDistrictCard;
	private bool _observatoryDistrictHaveCard;
	private int _cardsHeld;

	// Suit pieces
	private GameObject _chest;
	private GameObject _hatGoggles;
	private GameObject _lBoot;
	private GameObject _rBoot;

	// Puzzle bools while wearing suit pieces for objective
	private bool _musicPuzzleSuit;
	private bool _musicSuitIntroDone;
	private bool _theaterPuzzleSuit;
	private bool _observatoryPuzzleSuit;

	// Totorial complete
	private bool _tutorial;

	// Puzzles defeated for pass 4
	private bool _musicDefeated;
	private bool _theaterDefeated;
	private bool _observatoryDefeated;

	private bool _leftKey;
	private bool _midKey;
	private bool _rightKey;

	public float flashBangLife = 3.0f;
	public Vector3 flashBangHeight = new Vector3(0, 6.0f, 0);
	public float flashBangDistance = 3.0f;

	private ParticleSystem beam;
	public float beamLife = 4.0f;
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
		this._tutorial = false;
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
			this._chest = GameObject.FindWithTag("SuitPiece_Chest");
			this._hatGoggles = GameObject.FindWithTag("SuitPiece_HatAndGoggles");
			this._lBoot = GameObject.FindWithTag("SuitPiece_LBoot");
			this._rBoot = GameObject.FindWithTag("SuitPiece_RBoot");
			if (this.GetGameState() != GameStates.Suit)
			{
				this._chest.SetActive(false);
				this._hatGoggles.SetActive(false);
				this._rBoot.SetActive(false);
				this._lBoot.SetActive(false);
			}
			_player.PlayerActionNormal();
			_player.playerState = PlayerState.Normal;
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

	public bool GetFromPuzzle()
	{
		return this._fromPuzzle;
	}

	public void SetFromPuzzle()
	{
		this._fromPuzzle = true;
	}

	public void ResetFromPuzzle()
	{
		this._fromPuzzle = false;
	}

	public bool GetFromMusicDistrict()
	{
		return this._fromMusicDistrict;
	}
	
	public void SetFromMusicDistrict()
	{
		this._fromMusicDistrict = true;
	}
	
	public void ResetFromMusicDistrict()
	{
		this._fromMusicDistrict = false;
	}

	public bool GetFromTheaterDistrict()
	{
		return this._fromTheaterDistrict;
	}
	
	public void SetFromTheaterDistrict()
	{
		this._fromTheaterDistrict = true;
	}
	
	public void ResetFromTheaterDistrict()
	{
		this._fromTheaterDistrict = false;
	}

	public bool GetFromObservatoryDistrict()
	{
		return this._fromObservatoryDistrict;
	}
	
	public void SetFromObservatoryDistrict()
	{
		this._fromObservatoryDistrict = true;
	}
	
	public void ResetFromObservatoryDistrict()
	{
		this._fromObservatoryDistrict = false;
	}
	
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
		this._audioManager.ChangeVolume(AudioID.TokenUse, .3f);
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
		this._audioManager.ChangeVolume(AudioID.TokenUse, .3f);
		this._audioManager.PlayAudio(AudioID.TokenUse);
		this._HUDManager.CompassTrue(this.compass);
	}

	public bool GetCompass()
	{
		return this.compass;
	}
	
	public void LanternTrue()
	{
		this.lantern = true;
		this._audioManager.ChangeVolume(AudioID.TokenUse, .3f);
		this._audioManager.PlayAudio(AudioID.TokenUse);
		this._HUDManager.LanternTrue(this.lantern);
	}

	public void DreamsTrue()
	{
		this._dreams = true;
	}

	public bool GetDreams()
	{
		return this._dreams;
	}
	
	public bool GetLantern()
	{
		return this.lantern;
	}

	public bool CanFight()
	{
		return this._canFight;
	}

	public void CanFightTrue()
	{
		this._canFight = true;
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

	public void FirstTimeMusicPuzzle()
	{
		this._musicFirstTime = true;
	}

	public bool GetFirstTimeMusic()
	{
		return this._musicFirstTime;
	}

	public void OMalleyMusicPuzzleDone()
	{
		this._musicFirstTime = false;
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

	public bool IsTheaterIntro()
	{
		return this._theaterIntro;
	}

	public void TheaterIntroHeard()
	{
		this._theaterIntro = true;
	}

	public void FirstTimeTheaterPuzzle()
	{
		this._theaterFirstTime = true;
	}

	public bool GetFirstTimeTheater()
	{
		return this._theaterFirstTime;
	}

	public void OMalleyTheaterPuzzleDone()
	{
		this._theaterFirstTime = false;
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

	public bool IsObsIntro()
	{
		return this._obsIntro;
	}

	public void ObsIntroHeard()
	{
		this._obsIntro = true;
	}

	public void FirstTimeObservatoryPuzzle()
	{
		this._observatoryFirstTime = true;
	}

	public bool GetFirstTimeObservatory()
	{
		return this._observatoryFirstTime;
	}

	public void OMalleyObservatoryPuzzleDone()
	{
		this._observatoryFirstTime = false;
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

	public bool GetFirstTailorInteraction()
	{
		return this._firstTailorInteraction;
	}

	public void TailorSpokenTo()
	{
		this._firstTailorInteraction = true;
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

	public int CardsHeld()
	{
		return this._cardsHeld;
	}

	// Dumb, unnecessary extra work
	public void RemoveCards(string inMusic, string inTheater, string inObservatory)
	{
		if (this._musicDistrictCard != inMusic)
		{
			this._musicDistrictHaveCard = false;
		}
		if (this._theaterDistrictCard != inTheater)
		{
			this._theaterDistrictHaveCard = false;
		}
		if (this._observatoryDistrictCard != inObservatory)
		{
			this._observatoryDistrictHaveCard = false;
		}

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

	public void SuitWorn()
	{
		this._HUDManager.SuitTrue();
		this._cardsHeld = 0;
		this._stateManager.ChangeGameState(GameStates.Suit);
		this._chest.SetActive(true);
		this._hatGoggles.SetActive(true);
		this._lBoot.SetActive(true);
		this._rBoot.SetActive(true);
	}

	public bool GetMusicPuzzleVisitedSuit()
	{
		return this._musicPuzzleSuit;
	}

	public void MusicPuzzleVisitedSuit()
	{
		this._musicPuzzleSuit = true;
	}

	public bool GetMusicSuitIntro()
	{
		return this._musicSuitIntroDone;
	}

	public void MusicSuitIntroDone()
	{
		this._musicSuitIntroDone = true;
	}

	public bool GetTheaterPuzzleVisitedSuit()
	{
		return this._theaterPuzzleSuit;
	}
	
	public void TheaterPuzzleVisitedSuit()
	{
		this._theaterPuzzleSuit = true;
	}

	public bool GetObservatoryPuzzleVisitedSuit()
	{
		return this._observatoryPuzzleSuit;
	}
	
	public void ObservatoryPuzzleVisitedSuit()
	{
		this._observatoryPuzzleSuit = true;
	}
	
	// When suit removed, player now able to fight shadow creatures
	public void SuitRemoved()
	{
		this._HUDManager.SuitFalse();
		this._stateManager.ChangeGameState(GameStates.Normal);
		this._canFight = true;
		this._chest.SetActive(false);
		this._hatGoggles.SetActive(false);
		this._lBoot.SetActive(false);
		this._rBoot.SetActive(false);
	}

	public bool GetLeftKey()
	{
		return this._leftKey;
	}
	
	public bool GetMidKey()
	{
		return this._midKey;
	}
	
	public bool GetRightKey()
	{
		return this._rightKey;
	}
	
	public void LeftKeyAcquired()
	{
		this._leftKey = true;
	}
	
	public void MidKeyAcquired()
	{
		this._midKey = true;
	}
	
	public void RightKeyAcquired()
	{
		this._rightKey = true;
	}

	#endregion

	#region PuzzleProgression
	
	public bool GetMusicActivated()
	{
		return this._musicPuzzleActivated;
	}
	
	public void MusicPuzzleActivated()
	{
		this._musicPuzzleActivated = true;
	}

	public bool GetPuppetActivated()
	{
		return this._puppetPuzzleActivated;
	}

	public void PuppetPuzzleActivated()
	{
		this._puppetPuzzleActivated = true;
	}

	public bool GetObsActivated()
	{
		return this._observatoryPuzzleActivated;
	}

	public void ObsPuzzleActivated()
	{
		this._observatoryPuzzleActivated = true;
	}

	public bool IsTutorialComplete()
	{
		return this._tutorial;
	}

	public void TutorialCompleted()
	{
		this._tutorial = true;
	}

	public bool IsMusicDefeated()
	{
		return this._musicDefeated;
	}

	public void MusicPuzzleDefeated()
	{
		this._musicDefeated = true;
	}

	public bool IsTheaterDefeated()
	{
		return this._theaterDefeated;
	}

	public void TheaterPuzzleDefeated()
	{
		this._theaterDefeated = true;
	}

	public bool IsObservatoryDefeated()
	{
		return this._observatoryDefeated;
	}

	public void ObservatoryPuzzleDefeated()
	{
		this._observatoryDefeated = true;
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
		this._stateManager.ChangeGameState(state);
	}

	#endregion

	///////////////////////////////////////////////////////////////////
	///////////////////// HUD MANAGER FUNCTIONS ///////////////////////
	///////////////////////////////////////////////////////////////////

//	public void HUDSceneStart()
//	{
//		this._HUDManager.HUDSceneStart(token, lantern);
//	}

	public void EncounterClear()
	{
		this._HUDManager.EncounterClear();
	}

	public void PuppetPuzzleEncounterClear()
	{
		this._HUDManager.PuppetPuzzleEncounterClear();
	}

	public void NewWhiteOut()
	{
		this._HUDManager.NewWhiteOut();
	}

	public void ResetWhiteSpeed()
	{
		this._HUDManager.ResetWhiteSpeed();
	}

	public void ChangeObjective(GameObject gObj)
	{
		this._HUDManager.ChangeObjective(gObj);
	}

	/// Add Item SplashScreen
	public void StartItemInteraction(Sprite inSprite , string Itemtext = null)
	{
		this._HUDManager.StartItemInteraction(inSprite, Itemtext);
	}

	public void EndItemInteraction(bool inResponse)
	{
		this._HUDManager.EndItemInteraction(inResponse);
	}

	public void StartCardInteraction(Sprite inSprite, int inDist, GameObject inCardObj)
	{
		this._HUDManager.StartCardInteraction(inSprite, inDist, inCardObj);
	}

	public void EnableCardResponseOptions()
	{
		this._HUDManager.EnableCardResponseOptions();
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

	public void PulseLantern()
	{
		this._HUDManager.PulseLantern();
	}

	public void IdleLantern()
	{
		this._HUDManager.IdleLantern();
	}

	public void OMalleyEncounter()
	{
		this._HUDManager.OMalleyEncounter();
	}

	public void SetupScreenFade ()
	{
		this._HUDManager.SetupScreenFade();
	}

	public void ClearScreenFade()
	{
		this._HUDManager.ClearScreenFade();
	}

	public void FadebyAmount (Color NewColor, float DeltaTime)
	{
		this._HUDManager.FadeScreenByAmount(NewColor, DeltaTime);
	}

	public void PauseScreenFade()
	{
		this._HUDManager.PauseScreenFade();
	}

	public void ResumeScreenFade()
	{
		this._HUDManager.ResumeScreenFade();
	}

	public void EndGameImageOn()
	{
		this._HUDManager.EndGameImageOn();
	}

	public void WASDSetup()
	{
		this._HUDManager.WASDSetup();
	}

	public void TurnOffWASD()
	{
		this._HUDManager.TurnOffWASD();
	}
	
	///////////////////////////////////////////////////////////////////
	////////////////// ENCOUNTER MANAGER FUNCTIONS ////////////////////
	///////////////////////////////////////////////////////////////////

    #region EncounterManager

	public void SetEnemyActionNotVisible()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			if (enemy.GetComponent<BasicEnemyController>() != null)
			{
				enemy.GetComponent<BasicEnemyController>().NotVisibleAction();
			}
			this.UnpauseAudio(AudioID.Whispers);
		}
	}

	public void SetEnemyActionHidden()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			enemy.GetComponent<BasicEnemyController>().HiddenAction();
		}
	}
	
	public void SetEnemyActionHiddenTile()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			enemy.GetComponent<BasicEnemyController>().HiddenTileAction();
		}
	}

	public void SetEnemyActionSuit()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			enemy.GetComponent<BasicEnemyController>().SuitAction();
		}
	}

	public void SetEnemyActionInteraction()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			if (enemy.GetComponent<BasicEnemyController>() != null)
			{
				enemy.GetComponent<BasicEnemyController>().InteractionAction();
			}
			this.PauseAudio(AudioID.Whispers);
		}
	}

	public EncounterState GetEncounterState()
	{
		return this._encounterManager.GetState();
	}

	public void Encounter(GameObject enemy)
	{
		this._encounterManager.Encounter(enemy);
	}

	public void MusicPuzzleEncounter(GameObject enemy)
	{
		this._encounterManager.MusicPuzzleEncounter(enemy);
	}

	SewerController oicysController;

	public void StartOicysEncounter(SewerController inCont)
	{
		oicysController = inCont;
		this._encounterManager.OicysEncounter();
		this._stateManager.ChangeGameState (GameStates.Encounter);
		this._HUDManager.EnableEncounterView();
		this._HUDManager.SetClearStatus(false);
		this._player.PlayerActionOicysEnc();
	}

	public void StopOicysEncounter()
	{
		this._stateManager.ChangeGameState(GameStates.Normal);
		this._HUDManager.DisableEncounterView();
		this._player.PlayerActionNormal();
		oicysController.EncounterWon();
		this._HUDManager.DisableEncounterView();
	}

	public void StartEncounterMode()
	{
		if (this._stateManager.GameState() != GameStates.Encounter) 
		{
			this._stateManager.ChangeGameState (GameStates.Encounter);
		}
		
		this._HUDManager.EnableEncounterView();
		this._HUDManager.SetClearStatus(false);
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
		if (GameDirector.instance.GetGameState() != GameStates.Normal)
		{
			if (this.GetGameState() != GameStates.Suit)
			{
				this._stateManager.ChangeGameState(GameStates.Normal);
			}
			this._HUDManager.DisableEncounterView();
			this._player.PlayerActionNormal();
		}
	}

	public void StartMusicPuzzleEncounter()
	{
		if (this._stateManager.GameState() != GameStates.Encounter) 
		{
			this._stateManager.ChangeGameState (GameStates.Encounter);
		}
		this._HUDManager.EnableEncounterView();
		this._HUDManager.SetClearStatus(false);
		if (this._canFight)
		{
			this._player.PlayerActionMusicPuzzle();
		}
		else
		{
			this._player.PlayerActionNoFighting();
		}
	}

	public void PuppetPuzzleWhiteOut()
	{
		this._HUDManager.SetClearStatus(false);
		this._HUDManager.EnablePuppetPuzzleEncounterView();
	}

	public void PuppetPuzzleEncounter()
	{
		this._encounterManager.PuppetPuzzleEncounter();
	}
	
	public void StartPuppetPuzzleEncounter()
	{
		if (this._stateManager.GameState() != GameStates.Encounter) 
		{
			this._stateManager.ChangeGameState (GameStates.Encounter);
		}
		//this._HUDManager.EnablePuppetPuzzleEncounterView();
		//this.SetClearStatus(false);
		if (this._canFight)
		{
			this._player.PlayerActionPuppetPuzzle();
		}
		else
		{
			this._player.PlayerActionNoFighting();
		}
	}

	public void ObsPuzzleEncounter()
	{
		this._encounterManager.ObsPuzzleEncounter();
	}

	public void StartObsPuzzleEncounter()
	{
		if (this._stateManager.GameState() != GameStates.Encounter) 
		{
			this._stateManager.ChangeGameState (GameStates.Encounter);
		}
		this._HUDManager.EnableEncounterView();
		this._HUDManager.SetClearStatus(false);
		if (this._canFight)
		{
			this._player.PlayerActionObsPuzzle();
		}
		else
		{
			this._player.PlayerActionNoFighting();
		}
	}

	public void TakeSafteyLight()
	{
		if (!this._tokenUsed)
		{
			this._audioManager.ChangeVolume(AudioID.TokenUse, .3f);
			this._audioManager.PlayAudio(AudioID.TokenUse);
			this._tokenUsed = true;
			// Beam
			GameObject soteriaBeam = Instantiate(Resources.Load("ParticleEffects/Beam")) as GameObject;
			beam = soteriaBeam.GetComponent<ParticleSystem>();
			beam.transform.position = this.GetPlayer().gameObject.transform.position + beamHeight;
			beam.Play();

			string level;

//			
//			switch (_hubPhase)
//			{
//			case 5:
//				level = "HUBPass3";
//				StartCoroutine(BeamLevel(level, beamLife));
//				break;
//			case 1:
//				level = "HUBPass1";
//				StartCoroutine(BeamLevel(level, beamLife));
//				break;
//			case 2:
//				level = "HUBPass2";
//				StartCoroutine(BeamLevel(level, beamLife));
//				break;
//			case 3:
//				level = "HUBPass3";
//				StartCoroutine(BeamLevel(level, beamLife));
//				break;
//			case 4:
//				level = "HUBPass4";
//
//				break;
//			}

			StartCoroutine(BeamLevel("HUBPass1", beamLife));
			Destroy(soteriaBeam, beamLife);
		}
	}

	IEnumerator BeamLevel(string inLevel, float inBeamLife)
	{
		yield return new WaitForSeconds(inBeamLife);
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

	public bool IsTokenUsed()
	{
		return this._tokenUsed;
	}

	public void FindEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			enemy.SetActive(false);
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
		this._HUDManager.SetClearStatus(false);
	}

	public void PlayerOvercame()
	{
		//GetPlayer().GetComponent<Player>().Overcome();
		this._encounterManager.PlayerOvercame();
		this._stateManager.ChangeGameState(GameStates.Normal);
	}

	public void PlayerOvercameMusic()
	{
		this._encounterManager.PlayerOvercameMusic();
		this._stateManager.ChangeGameState(GameStates.Normal);
	}

	public void PlayerOvercameOicys()
	{
		this._encounterManager.PlayerOvercameOicys();
		this._stateManager.ChangeGameState(GameStates.Normal);
	}

	public void PuzzleOvercome()
	{
		this._encounterManager.PuzzleOvercome();
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
		this.IdleLantern();
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

	public LanternController GetLanternController()
	{
		return this._lanternController;
	}

	void RechargeLantern()
	{
		this._lanternController.RechargeLantern();
	}

	public void CheckLantern()
	{
		if (this._lanternController != null && this.lantern)
		{
			this.RechargeLantern();
		}
	}

	public void EncounterOver()
	{
		// reset encounter for game over
		this._encounterManager.StopEncounterFromItem();
		this.GameOver();
	}

	public void GameOver()
	{
		this.FindEnemies();
		this.StopEncounterMode();
		this.ClearAudioList();
		// Player never spoke to Fortune teller to receive token -> punish and reset game
		// || player gave up token for key piece (in phase 4 of game)
//		if (!this.token && this.GetGamePhase() != 4)
//		{
			Application.LoadLevel("Harbor");
//		}
//		else
//		{
//			Application.LoadLevel("HarborRespawn");
//		}
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

	public void Rewind(AudioID inAID)
	{
		this._audioManager.Rewind(inAID);
	}

	public void ClearAudioList()
	{
		this._audioManager.ClearAudioList();
	}

	public void ChangeVolume(AudioID inAID, float inVolume)
	{
		this._audioManager.ChangeVolume(inAID, inVolume);
	}
	
	public void AddVolumePuzzle(AudioID inAID, float inVolume)
	{
		this._audioManager.AddVolumePuzzle(inAID, inVolume);
	}
	
	public void SubtractVolumePuzzle(AudioID inAID, float inVolume)
	{
		this._audioManager.SubtractVolumePuzzle(inAID, inVolume);
	}

	public void DefeatedMusicTile(AudioID inAID)
	{
		this._audioManager.DefeatedMusicTile(inAID);
	}
	
	public void OvercomeMusicPuzzle(AudioID inAID)
	{
		this._audioManager.OvercomeMusicPuzzle(inAID);
	}

	public void UpdateAudioClipSequentialProperty(string inAid, bool inSquential)
	{
		AudioID aid = this._audioManager.getIDByName(inAid);
		this._audioManager.FindAudioSrcbyID (aid).IsSequential = inSquential;
	}

	public void LoadClipinSequence(AudioID inAid, int indx)
	{
		this._audioManager.FindAudioSrcbyID (inAid).LoadClipinCollection(indx);
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
	public void AttachAudioSource( GameObject inGameObj, string inName)
	{
		this._audioManager.AttachAudioSource (inGameObj,inName);
	}

	public bool isClipPlaying(AudioID inAID)
	{
		return this._audioManager.isClipPlaying(inAID);
	}
	
	public void CollectAudioClipsForDialogue(string inFolderName)
	{
		AudioID aid = this._audioManager.getIDByName(inFolderName);
		this._audioManager.CollectDialogueAudioClips(inFolderName, aid);
	}
	
	public void CollectAudioClips(string inFolderName)
	{
		AudioID aid = this._audioManager.getIDByName(inFolderName);
		this._audioManager.CollectAudioClips(inFolderName, aid);
	}

	public AudioID getIDByName (string inName)
	{
		return this._audioManager.getIDByName(inName);
	}
	
	///Other Stuff to Add.
	/// --->
	//set Parameters Methods
	//Remove Audio Clip
	//Silence Audio Clip
	//Clone Audio Clip

	public float GetPuzzleWinVolume()
	{
		return this._audioManager.GetPuzzleWinVolume();
	}
	
	public float GetVolume(AudioID inAID)
	{
		return this._audioManager.GetVolume(inAID);
	}

	public void FadeIn(AudioID inAID)
	{
		this._audioManager.FadeIn(inAID);
	}

	public void StartFadeInTimer()
	{
		this._audioManager.StartFadeInTimer ();
	}

	public void FadeOut(AudioID inAID)
	{
		this._audioManager.FadeOut(inAID);
	}

	public void StartFadeOutTimer()
	{
		this._audioManager.StartFadeOutTimer ();
	}

	public void PauseAudio(AudioID inAID)
	{
		this._audioManager.PauseAudio(inAID);
	}

	public void UnpauseAudio(AudioID inAID)
	{
		this._audioManager.UnpauseAudio(inAID);
	}
	
	#endregion

	///////////////////////////////////////////////////////////////////
	////////////////////// INVENTORY FUNCTIONS ////////////////////////
	///////////////////////////////////////////////////////////////////

	#region Inventory Functions

	public void StartKeyInteraction(Sprite inSprite)
	{
		this._HUDManager.StartKeySwapInteraction(inSprite);
	}

	public void EndKeyInteraction()
	{
		this._HUDManager.EndKeySwapInteraction();

	}
	public void SwapItemForKey(ItemType inType)
	{
		switch(inType)
		{
			case ItemType.Token:
				//this._HUDManager.SwapTokenForKey();
				this.token = false;
			break;
			
			case ItemType.Compass:    
				//this._HUDManager.SwapCompassForKey();
				this.compass = false;
			break;
			
			case ItemType.Lantern:
				//this._HUDManager.SwapLanternForKey();
				this.lantern = false;
			break;
		}
		//Replacing all the commented out calls above. Happened in HUD manager port. 
		this._HUDManager.SwapForKey (inType);
	}

	#endregion

	///////////////////////////////////////////////////////////////////
	//////////////////////// STATUE CRUMBLE ///////////////////////////
	///////////////////////////////////////////////////////////////////
	
	#region Statue Crumble Functions

	private bool _statueCrumbled;

	public StatueCrumbleState _crumbleSate;

	public StatueCrumbleState GetStatueCrumble()
	{
		return this._crumbleSate;
	}

	public void StatueCrumbleOne()
	{
		this._crumbleSate = StatueCrumbleState.CRUMBLEONE;
	}

	public void StatueCrumbleTwo()
	{
		this._crumbleSate = StatueCrumbleState.CRUMBLETWO;
	}

	public void StatueCrumbleThree()
	{
		this._crumbleSate = StatueCrumbleState.CRUMBLETHREE;
	}

	public void StatueCrumbled()
	{
		this._crumbleSate = StatueCrumbleState.CRUMBLED;
	}

	#endregion

	///////////////////////////////////////////////////////////////////
	////////////////// DIALOGUE MANAGER FUNCTIONS /////////////////////
	///////////////////////////////////////////////////////////////////
	
	#region DialogueManager Methods

	public void GetDialogueFromChoice(int iChoice)
	{
		this._dialoguemanager.GetDialogueFromChoice( iChoice );
	}

	public GameObject getDialogueInterface()
	{
		return null;
	}
	
	public bool isDialogueActive()
	{
		return this._dialoguemanager.isDialogueActive();
	}

	public void SetupDialogue(string txtname, GameObject inActor = null)
	{
		this._dialoguemanager.ReloadDialogueData(txtname, inActor);
	}

	public void SetupDialogueNPC(Sprite NpcPortrait)
	{
		this._dialoguemanager.SetNpcPortrait(NpcPortrait);
	}

	public void SetupDialogueChoices (string fChoice, string sChoice, string tChoice)
	{
		this._dialoguemanager.LoadChoicesDialogueName(fChoice, sChoice, tChoice);
	}

	public void GetDialogueFromReaction(string inFolderName, GameObject inActor = null)
	{
		this._dialoguemanager.GetDialogueFromReaction(inFolderName, inActor);
	}

	public void StartDialogue( bool CanSkipDialogue = false)
	{
		this._dialoguemanager.StartDialogue( CanSkipDialogue );
	}

	public void EndDialogue()
	{
		this._dialoguemanager.EndDialogue();
	}

	public void SkipLine()
	{
		this._dialoguemanager.SkipLine();
	}

	public void EndTriggerState()
	{
		this._dialoguemanager.EndTriggerState();
	}

	public DialogueState GetDiagState()
	{
		return this._dialoguemanager.GetDiagState();
	}

	#endregion
}