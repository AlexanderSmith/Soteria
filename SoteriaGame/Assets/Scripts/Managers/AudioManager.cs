using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	public float mastervolume;
	public bool mute;
	public float puzzleWinVolume;

	AudioSourceWrapper DiagAudioSrc;

	Timer puzzlefadeTimer;
	Timer fadeInTimer;
	Timer fadeOutTimer;
	
	private List<AudioSourceWrapper> _audioSourceList;
	
	// Use this for initialization
	void Awake ()
	{
		this.enabled = false;
		_audioSourceList = new List<AudioSourceWrapper>();
		//TimerManager.instance.Attach(fadeTimer, TimersType.Puzzle);
		// Not working
//		fadeInTimer = TimerManager.instance.Attach(TimersType.FadeIn);
//		fadeOutTimer = TimerManager.instance.Attach(TimersType.FadeOut);
	}

	public void AddAudioSource(string inClipName, AudioID inAID, GameObject inGameObj)
	{
		if (inGameObj == null)
			inGameObj = GameObject.Find ("Player");

		AudioSource audioSrc = inGameObj.GetComponent<AudioSource>();

		if (audioSrc == null)
			audioSrc = inGameObj.AddComponent<AudioSource> ();
		   
		audioSrc.clip = Resources.Load ("Audio/" + inClipName) as AudioClip;
		audioSrc.playOnAwake = false;
		
		this._audioSourceList.Add (new AudioSourceWrapper(inGameObj, audioSrc, inAID));
	}

	public void AttachAudioSource( GameObject inGameObj, string inName)
	{
		AudioID  aID = this.getIDByName(inName);

		if (this.FindAudioSrcbyID(aID) == null)
		{
			AudioSource AudioSrc = inGameObj.GetComponent<AudioSource>() as AudioSource;
			this._audioSourceList.Add (new AudioSourceWrapper(inGameObj, AudioSrc, aID));
		}
	}
		

	public AudioID getIDByName(string inName)
	{
		AudioID AID = ((AudioID)System.Enum.Parse(typeof(AudioID), inName));
		
		return AID;
	}
	
	public AudioSourceWrapper FindAudioSrcbyID (AudioID inAID)
	{
		foreach (AudioSourceWrapper ASW in _audioSourceList)
		{
			if (ASW.getAID().Equals(inAID))
				return ASW;
		}
		
		return null;
	}

	public void Initialize()
	{

	}
	
	public void Update()
	{
		
	}

	/// Call in Case the audio is for a dialogue.
	public void CollectDialogueAudioClips(string foldername, AudioID inAID)
	{
		string resdir = "DialoguesSrc/" + foldername + "/Audio/"; 
		privCollectAudioClips (resdir, inAID);
	}
	/// Call for sequential audio.
	public void CollectAudioClips (string foldername, AudioID inAID) 
	{
		string resdir = "Audio/" + foldername + "/";
		privCollectAudioClips (resdir, inAID);
	}
	private void privCollectAudioClips (string resdir, AudioID inAID)
	{
		AudioSourceWrapper adsrc = this.FindAudioSrcbyID(inAID);
		
		foreach(AudioClip AC in Resources.LoadAll(resdir, typeof(AudioClip)))
		{
			adsrc.AddClip(AC);
		}
	}
	public bool isClipPlaying(AudioID inAid)
	{
		return FindAudioSrcbyID(inAid).IsPlaying();
	}
	
	// Maybe implement a different Data Structure in the future for largers sets of data Hashtable or Dictionary. 
	public void PlayAudio(AudioID inAID)
	{
		FindAudioSrcbyID(inAID).playClip();
	}

	public void StopAudio(AudioID inAID)
	{
		FindAudioSrcbyID(inAID).stopClip();
	}

	// Before level loaded
	public void ClearAudioList()
	{
		this._audioSourceList.Clear();
	}

	public void ChangeVolume(AudioID inAID, float inVolume)
	{
		FindAudioSrcbyID(inAID).updateVolume(inVolume);
	}
	
	public void AddVolumePuzzle(AudioID inAID, float inVolume)
	{
		FindAudioSrcbyID(inAID).addVolumePuzzle(inVolume);
	}
	
	public void SubtractVolumePuzzle(AudioID inAID, float inVolume)
	{
		FindAudioSrcbyID(inAID).subtractVolumePuzzle(inVolume);
	}

	public void DefeatedMusicTile(AudioID inAID)
	{
		FindAudioSrcbyID(inAID).defeatedMusicTile();
	}
	
	public void OvercomeMusicPuzzle(AudioID inAID)
	{
		FindAudioSrcbyID(inAID).overcomeMusicPuzzle();
	}
	
	public float GetPuzzleWinVolume()
	{
		return this.puzzleWinVolume;
	}
	
	public float GetVolume(AudioID inAID)
	{
		return FindAudioSrcbyID(inAID).getVolume();
	}

	public void FadeOut(AudioID inAID)
	{
		// if not elapsed time then fade
		// lerp volume, don't call below
		//FindAudioSrcbyID (inAID).fadeOut();
		float volume = FindAudioSrcbyID (inAID).getVolume();
		fadeOutTimer.StartTimer();
		if (fadeOutTimer.ElapsedTime() <= 5.0f)
		{
			volume = Mathf.Lerp (volume, 0.0f, Time.deltaTime);
			FadeOut(inAID);
		}
		else
		{
			fadeOutTimer.ResetTimer();
		}
	}

	public void FadeIn(AudioID inAID)
	{
		// if not elapsed time then fade
		// lerp volume
		//FindAudioSrcbyID (inAID).fadeIn();
		float volume = FindAudioSrcbyID (inAID).getVolume();
		fadeInTimer.StartTimer();
		if (fadeInTimer.ElapsedTime() <= 5.0f)
		{
			volume = Mathf.Lerp (volume, 1.0f, Time.deltaTime);
			FadeIn(inAID);
		}
		else
		{
			fadeInTimer.ResetTimer();
		}
	}
}

// Requires separtate script for future stuffies
// Enum names need to match Object name in Unity
public enum AudioID
{
	None,
	Dialogue,
	WoodFootsteps,
	StoneFootsteps,
	BackgroundIntro,
	BackgroundHarbor,
	BackgroundHub,
	BackgroundMusic,
	BackgroundTheater,
	BackgroundObservatory,
	Heartbeats,
	LeavingHide,
	TokenUse,
	OrganMusicComplete,
	OrganMusic,
	BrassMusic,
	StringMusic,
	WindMusic,

	//Dialogue
	After_Ana_Gets_Overwhelmed_in_MusicHUBp2,
	After_Ana_Gets_Overwhelmed_in_ObservatoryHUBp4,
	After_Ana_Gets_Overwhelmed_in_TheaterHUBp3,
	AnaAfterSuitOff,
	AnaAfterThreeFailsObservPuzz,
	AnaBackpackJournalPickupOBSERp2,
	AnaChameleonTHEATERp2,
	AnaDistEnterVOMUSICp3,
	AnaDistEnterVOOBSERp1,
	AnaDistEnterVOTHEATERp1,
	AnaDroppingItemAfterMusicPuzz,
	AnaDuringOMalleyFear,
	AnaEggshellsPickupMUSICp2,
	AnaEnteringMusicPuzFirstTime,
	AnaEnteringMusicPuzzWithSuit,
	AnaEnteringObservPuzzFirstTime,
	AnaEnteringObservPuzzSecondTime,
	AnaEnteringTheaterPuzzFirstTime,
	AnaEnteringTheaterPuzzSecondTime,
	AnaEnteringTheaterPuzzSuit,
	AnaFailingLingerAgainstOMalley,
	AnaFirstAnswerTakingOffSuit,
	AnaFirstHarborSCEncounter,
	AnaHandDollPickupMUSICp2,
	AnaHubBeforeEnteringDistrict,
	AnaMusicGateRattleResponseOne,
	AnaMusicGateRattleResponseTwo,
	AnaObservPuzzAtDoorSuit,
	AnaObservPuzzFirstLinger,
	AnaObservPuzzSecondLinger,
	AnaObservPuzzThirdLinger,
	AnaOMalleyAfterLingerSuccess,
	AnaOMalleyAtTailorWearingSuitHubp5,
	AnaOMalleyDiscussMusicPuzzFail,
	AnaPartyHatTHEATERp2,
	AnaPearlsTHEATERp2,
	AnaSecondAnswerTakingOffSuit,
	AnaSoteriaPrayer1HUBp4,
	AnaSoteriaPrayer2HUBp4,
	AnaStarChartPickupOBSERVp2,
	AnaTailorCollectedCorrectCardsHubp4,
	AnaTailorFirstDialogueHubp4,
	AnaTheaterFirstProvoke,
	AnaTheaterPuzzFirstLinger,
	AnaTheaterPuzzSecondLinger,
	AnaTheaterPuzzThirdLinger,
	AnaTheaterSecondProvoke,
	AnaTrainTicketOBSERVp2,
	AnaTriesGateMUSICp1,
	AnaWhistlePickupMUSICp2,
	AnaWithSuitObservPuzzEnd,
	CartFirstEncounterHUBp1,
	FortuneTellerTokenDialogue,
	FTAnaFailUsingTokenHAR,
	IntroDialogue,
	MusicPuzzFirstLingerResponse,
	MusicPuzzFourthLingerResponse,
	MusicPuzzSecondLingerResponse,
	MusicPuzzThirdLingerResponse,
	OMalleyAboutSoteria,
	OMalleyAfterCompletingMusicPuzz,
	OMalleyAnaLingerSuccess,
	OMalleyExitingObservPuzzSuccess,
	OMalleyExitTheaterPuzzSuccess,
	OMalleyHubp5BeforeObservatoryPuzzle,
	OMalleyMeow,
	OMalleyMusicGateMUSICp1,
	OMalleyResponsePrayer1,
	OMalleyResponsePrayer2,
	OMalleyResponseToNotTakingSuitOff,
	OMalleySoteriaStatueGate,
	OMalleyTeachingAnaToOvercomeFear1,
	OMalleyTeachingAnaToOvercomeFear2,
	OMalleyTeachingAnaToOvercomeFear3,
	OMalleyTeachingLinger,
	OMalleyTeachingPrevoke,
	OMalleyTransformIntoFear,
	PTFirstEncounter,
	PTLanternRecharge,
	TailorWrongCard,
	TakingSuitOffInHub,
	WhispersMusicPuzzleActivation,
	WhispersPuppetPuzzleActivation
}

public class AudioSourceWrapper
{
	private GameObject _gameobj;
	private AudioSource _audiosrc;
	private AudioID _aID;
	private int currindx;
	private List<AudioClip> _audioclips;
	public bool IsSequential = true;
	
	private AudioSourceWrapper(){}
	
	public AudioSourceWrapper(GameObject inGameObj, AudioSource inAudioSrc, AudioID inAID)
	{	
		this._gameobj = inGameObj;
		this._audiosrc = inAudioSrc;
		this._aID = inAID;
		this._audioclips = new List<AudioClip>();
	}
	
	public AudioID getAID()
	{
		return this._aID;
	}
	
	public void playClip()
	{
		if (this._audiosrc.clip == null)
		{
			if (_audioclips.Count > 0)
				this._audiosrc.clip = this._audioclips [0];
		}
		else 
		{
			if (!this._audiosrc.clip.Equals (this._audioclips [currindx]))
			{
				Nextclip ();
			}
		}
		Debug.Log (this._audiosrc.clip.name);
		this._audiosrc.Play();
		
		if (this._audioclips.Count > 1) 
			currindx++;
	}

	public void UpdateAudioClip (AudioClip inClip)
	{
		this._audiosrc.clip = inClip;
	}
	
	public bool IsPlaying ()
	{
		return _audiosrc.isPlaying;
	}
	
	public void AddClip( AudioClip inAudioClip)
	{
		this._audioclips.Add(inAudioClip);
	}
	
	public void Nextclip()
	{
		if (currindx < this._audioclips.Count && IsSequential) 
		{
			this._audiosrc.clip = this._audioclips [currindx];
		}
	}

	public void LoadClipinCollection (int indx)
	{

		if (indx < this._audioclips.Count)
		{
			currindx = indx;
			this._audiosrc.clip = this._audioclips[indx];
		}
	}

	public void Rewind()
	{
		this._audiosrc.clip = this._audioclips[0];
	}
	
	public void stopClip()
	{
		this._audiosrc.Stop();
	}
	
	public void updateVolume(float inVolume)
	{
		this._audiosrc.volume = inVolume;
	}
	
	public void addVolumePuzzle(float inVolume)
	{
		this._audiosrc.volume += inVolume;
		
		if (this._audiosrc.volume >= GameDirector.instance.GetPuzzleWinVolume())
		{
			GameDirector.instance.StopEncounterMode();
			GameDirector.instance.GetPlayer().ResetEncounter();
			GameDirector.instance.PlayerOvercame();
		}
	}
	
	public void subtractVolumePuzzle(float inVolume)
	{
		this._audiosrc.volume = Mathf.Lerp(this._audiosrc.volume, 0f, Time.deltaTime * inVolume);
		if (this._audiosrc.volume <= .001f && !GameDirector.instance.isDialogueActive())
		{
			GameDirector.instance.GameOver();
		}
	}

	public void defeatedMusicTile()
	{
		this._audiosrc.volume = Mathf.Lerp(this._audiosrc.volume, .025f, Time.deltaTime);
	}
	
	public void overcomeMusicPuzzle()
	{
		this._audiosrc.volume = Mathf.Lerp(this._audiosrc.volume, 1f, Time.deltaTime);
	}
	
	public float getVolume()
	{
		return this._audiosrc.volume;
	}

	public void fadeOut()
	{
		// sub volume
	}

	public void fadeIn()
	{
		// add volume
	}
}