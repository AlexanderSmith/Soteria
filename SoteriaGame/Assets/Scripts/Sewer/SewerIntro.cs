using UnityEngine;
using System.Collections;

public class SewerIntro : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaSewerIntro");
			GameDirector.instance.StartDialogue();
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				this.gameObject.SetActive(false);
			}
		}
	}
}