using UnityEngine;
using System.Collections;

public class IntroDialogue : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("IntroDialogue");
			GameDirector.instance.StartDialogue();
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.StopAudioClip(AudioID.BackgroundIntro);
				GameDirector.instance.ChangeVolume(AudioID.BackgroundHarbor, 1f);
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}