using UnityEngine;
using System.Collections;

public class IntroDialogue : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip(AudioID.BackgroundIntro);
		GameDirector.instance.PlayAudioClip(AudioID.BackgroundHarbor);
		GameDirector.instance.ChangeVolume(AudioID.BackgroundHarbor, 0.0f);
	}

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
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.StopAudioClip(AudioID.BackgroundIntro);
				GameDirector.instance.ChangeVolume(AudioID.BackgroundHarbor, 1f);
				GameDirector.instance.TurnOffWASD();
//				GameDirector.instance.FadeOut(AudioID.BackgroundIntro);
//				GameDirector.instance.FadeIn(AudioID.BackgroundHarbor);
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}