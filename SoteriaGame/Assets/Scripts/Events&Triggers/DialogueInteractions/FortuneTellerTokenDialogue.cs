using UnityEngine;
using System.Collections;

public class  FortuneTellerTokenDialogue: MonoBehaviour
{
	public Sprite NpcPortrait;
	public Reaction _reaction;

	void OnTriggerEnter(Collider player)
	{
<<<<<<< HEAD
		if ( player.GetType() == typeof( BoxCollider ) && player.gameObject.tag == "Player")
=======
		if (player.GetType() == typeof(BoxCollider) && player.gameObject.tag == "Player")
>>>>>>> refs/remotes/origin/WorkingBranch
		{
			//Debug.Log("collision");
			this.gameObject.GetComponentInChildren<InspectFortuneTeller>().TurnOffInspect();
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("FortuneTellerTokenDialogue");
			GameDirector.instance.SetupDialogueNPC(this.NpcPortrait);
			GameDirector.instance.StartDialogue(true);
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