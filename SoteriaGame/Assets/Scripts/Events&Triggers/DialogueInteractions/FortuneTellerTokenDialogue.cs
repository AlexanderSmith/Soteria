using UnityEngine;
using System.Collections;

public class  FortuneTellerTokenDialogue: MonoBehaviour
{
	private GameObject _npcportrait;
	public Reaction _reaction;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("FortuneTellerTokenDialogue");
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
				this._reaction.execute();
			}
		}
	}
}