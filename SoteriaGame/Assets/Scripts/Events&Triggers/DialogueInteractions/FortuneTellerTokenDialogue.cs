using UnityEngine;
using System.Collections;

public class  FortuneTellerTokenDialogue: MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("FortuneTellerTokenDialogue", AudioID.None);
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
				GameDirector.instance.TokenTrue();
			}
		}
	}
}
