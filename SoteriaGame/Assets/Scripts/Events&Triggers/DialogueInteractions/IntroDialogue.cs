using UnityEngine;
using System.Collections;

public class IntroDialogue : MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("IntroDialogue", AudioID.None);
		GameDirector.instance.StartDialogue();
	}

	void OnTriggerStay()
	{
		if (!GameDirector.instance.isDialogueActive())
		{
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			GameDirector.instance.StopAudioClip(AudioID.BackgroundIntro);
			GameDirector.instance.ChangeVolume(AudioID.BackgroundHarbor, .5f);
		}
	}
}