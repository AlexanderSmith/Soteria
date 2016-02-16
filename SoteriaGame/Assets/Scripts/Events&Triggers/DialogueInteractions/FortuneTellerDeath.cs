using UnityEngine;
using System.Collections;

public class FortuneTellerDeath : MonoBehaviour
{
	void Start()
	{
		if (!GameDirector.instance.GetToken())
		{
			this.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("FTAnaFailUsingTokenHAR");
			GameDirector.instance.StartDialogue();
			this.gameObject.GetComponentInChildren<InspectFortuneTeller>().TurnOffInspect();
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
