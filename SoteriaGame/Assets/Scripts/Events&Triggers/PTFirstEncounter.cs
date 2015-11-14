using UnityEngine;
using System.Collections;

public class  PTFirstEncounter: MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("PTFirstEncounter", AudioID.None);
		GameDirector.instance.StartDialogue();
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
}
