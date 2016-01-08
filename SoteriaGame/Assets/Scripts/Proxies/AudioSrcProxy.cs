using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioSrcProxy : MonoBehaviour {
	
	public string AudioSourceID;
	public bool IsDialogue;
	public bool IsSequential = true;

	/// <summary>
	/// Get the Audio Src from the objects and attaches it to the Audio Manager! then 
	/// it can be controlled from there
	/// </summary>
	void Awake ()
	{	
		//Temporary to avoid the code to break;
		if (AudioSourceID == "")
			AudioSourceID = this.gameObject.name;
		

		GameDirector.instance.AttachAudioSource(this.gameObject, AudioSourceID);		
		
		if (IsDialogue)
			GameDirector.instance.CollectAudioClipsForDialogue(AudioSourceID);
		else
			GameDirector.instance.CollectAudioClips(AudioSourceID);

		GameDirector.instance.UpdateAudioClipSequentialProperty(AudioSourceID,IsSequential);
	}
}