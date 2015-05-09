using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  first pass at an audio manager, The first idea I had was to create a manager that would keep track of all the audio sources we needed to play
///  a couple of problems rise up from this. Primarily the fact that designers won't be able to control audio that well. a quick solution to this is to
///  have a proxy script on the gameobject that has the audio source. 
/// 
///  This manager at least well work well with the Dialogue system, Music and the system audio. SFX are another issue that we can tackle in a later revision.
///  Worst case I can work closely with designers when it come to Audio.
/// </summary>
/// 
public class AudioManager : MonoBehaviour
{
	public float mastervolume;
	public bool mute;

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

	public void Initialize()
	{

	}

	public void Update()
	{

	}

	// Maybe implement a different Data Structure in the future for largers sets of data Hashtable or Dictionary. 
	public void PlayAudio(AudioID inAID)
	{
		for (int i = 0; i< this._audioSourceList.Count; i++)
		{
			if (this._audioSourceList[i].getAID().Equals(inAID))
			{
				this._audioSourceList[i].playClip();
				break;
			}
		}
	}

}
public enum AudioID
{
	Fire,
	BGM,
	Dialogue_1,
	Dialogue_2
}

public class AudioSourceWrapper
{
	private GameObject _gameobj;
	private AudioSource _audiosrc;
	private AudioID _aID;

	private AudioSourceWrapper(){}

	public AudioSourceWrapper(GameObject inGameObj, AudioSource inAudioSrc, AudioID inAID)
	{
		this._gameobj = inGameObj;
		this._audiosrc = inAudioSrc;
		this._aID = inAID;
	}

	public AudioID getAID()
	{
		return this._aID;
	}

	public void playClip()
	{
		this._audiosrc.Play();
	}


}