using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	public float mastervolume;
	public bool mute;
	public float puzzleWinVolume;

	AudioSourceWrapper DiagAudioSrc;
	
	private List<AudioSourceWrapper> _audioSourceList;
	
	// Use this for initialization
	void Awake ()
	{
		this.enabled = false;
		_audioSourceList = new List<AudioSourceWrapper>();
	}
	
	public void AddAudioSource(string inClipName, AudioID inAID, GameObject inGameObj)
	{
		if (inGameObj == null)
			inGameObj = GameObject.Find ("MCP");
		
		AudioSource audioSrc = inGameObj.AddComponent<AudioSource> ();
		audioSrc.clip = Resources.Load ("Audio/" + inClipName) as AudioClip;
		audioSrc.playOnAwake = false;
		
		this._audioSourceList.Add (new AudioSourceWrapper(inGameObj, audioSrc, inAID));
	}
	
	public void AttachAudioSource( AudioSource inAudioSrc, GameObject inGameObj, string inName)
	{
		AudioID  aID = this.getIDByName(inName);
		this._audioSourceList.Add (new AudioSourceWrapper(inGameObj, inAudioSrc, aID));
	}
	
	//I need to test this one!!
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
	
	public void AddVolume(AudioID inAID, float inVolume)
	{
		FindAudioSrcbyID(inAID).addVolume(inVolume);
	}
	
	public void SubtractVolume(AudioID inAID, float inVolume)
	{
		FindAudioSrcbyID(inAID).subtractVolume(inVolume);
	}
	
	public float GetPuzzleWinVolume()
	{
		return this.puzzleWinVolume;
	}
	
	public float GetVolume(AudioID inAID)
	{
		return FindAudioSrcbyID(inAID).getVolume();
	}
}

// Requires separtate script for future stuffies
// Enum names need to match Object name in Unity
public enum AudioID
{
	Dialogue,
	WoodFootsteps,
	StoneFootsteps,
	BackgroundMusic,
	Heartbeats,
	LeavingHide,
	TokenUse,
	OrganMusicBroken,
	OrganMusic,
	BrassMusic,
	StringMusic,
	WindMusic
}

public class AudioSourceWrapper
{
	private GameObject _gameobj;
	private AudioSource _audiosrc;
	private AudioID _aID;
	private int currindx;
	private List<AudioClip> _audioclips;
	
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
			this._audiosrc.clip = this._audioclips [0];
		else 
		{
			if (!this._audiosrc.clip.Equals (this._audioclips [currindx]))
				Nextclip ();
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
		if (currindx < this._audioclips.Count) 
		{
			this._audiosrc.clip = this._audioclips [currindx];
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
	
	public void addVolume(float inVolume)
	{
		this._audiosrc.volume += inVolume;
		
		if (this._audiosrc.volume >= GameDirector.instance.GetPuzzleWinVolume())
		{
			GameDirector.instance.StopEncounterMode();
			GameDirector.instance.PlayerOvercame();
		}
	}
	
	public void subtractVolume(float inVolume)
	{
		this._audiosrc.volume = Mathf.Lerp(this._audiosrc.volume, 0f, Time.deltaTime);
		if (this._audiosrc.volume <= .001f)
		{
			GameDirector.instance.GameOver();
		}
	}
	
	public float getVolume()
	{
		return this._audiosrc.volume;
	}
}