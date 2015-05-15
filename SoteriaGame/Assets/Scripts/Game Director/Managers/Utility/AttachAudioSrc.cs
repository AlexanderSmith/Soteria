using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AttachAudioSrc : MonoBehaviour {

	/// <summary>
	/// Get the Audi Src from the objects and attaches it to the Audio Manager! then 
	/// it can be controlled from there
	/// </summary>
	void Awake () {
	
		AudioSource AudioSrc =  this.gameObject.GetComponent<AudioSource>() as AudioSource;
		GameDirector.instance.AttachAudioSource(AudioSrc, this.gameObject, this.gameObject.name);

	}
}
