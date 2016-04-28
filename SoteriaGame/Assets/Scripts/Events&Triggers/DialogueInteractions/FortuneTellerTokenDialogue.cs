using UnityEngine;
using System.Collections;

public class  FortuneTellerTokenDialogue: MonoBehaviour
{
	public Sprite NpcPortrait;
	public Reaction _reaction;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("FortuneTellerTokenDialogue");
			GameDirector.instance.SetupDialogueNPC(this.NpcPortrait);
			GameDirector.instance.StartDialogue(true);
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