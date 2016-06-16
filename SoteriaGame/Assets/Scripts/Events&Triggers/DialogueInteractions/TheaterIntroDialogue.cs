using UnityEngine;
using System.Collections;

public class TheaterIntroDialogue : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && !GameDirector.instance.IsTheaterIntro())
		{
			GameDirector.instance.TheaterIntroHeard();
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaDistEnterVOTHEATERp1");
			GameDirector.instance.StartDialogue(true);
		}
	}
	
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}