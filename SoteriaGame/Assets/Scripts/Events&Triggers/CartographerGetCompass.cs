using UnityEngine;
using System.Collections;

public class  CartographerGetCompass: MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("CartFirstEncounterHUBp1", AudioID.None);
		GameDirector.instance.StartDialogue();
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
}
