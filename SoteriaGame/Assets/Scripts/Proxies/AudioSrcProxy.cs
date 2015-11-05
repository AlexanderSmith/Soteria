using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioSrcProxy : MonoBehaviour {
	
	public string AudioSourceID;
	public bool IsDialogue;
	
	/// <summary>
	/// Get the Audio Src from the objects and attaches it to the Audio Manager! then 
	/// it can be controlled from there
	/// </summary>
	void Awake ()
	{	
		//Temporary to avoid the code to break;
		if (AudioSourceID == "")
			AudioSourceID = this.gameObject.name;
		
		AudioSource AudioSrc =  this.gameObject.GetComponent<AudioSource>() as AudioSource;
		GameDirector.instance.AttachAudioSource(AudioSrc, this.gameObject, AudioSourceID);		
		
		if (IsDialogue)
			GameDirector.instance.CollectAudioClipsForDialogue(AudioSourceID,AudioSourceID);
		else
			GameDirector.instance.CollectAudioClips(AudioSourceID,AudioSourceID);
	}
}