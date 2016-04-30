using UnityEngine;
using System.Collections;

public class ObservatoryIntroDialogue : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && !GameDirector.instance.IsObsIntro())
		{
			GameDirector.instance.ObsIntroHeard();
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaDistEnterVOOBSERp1");
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