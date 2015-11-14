using UnityEngine;
using System.Collections;

public class  FortuneTellerTokenDialogue: MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("FortuneTellerTokenDialogue", AudioID.None);
		GameDirector.instance.StartDialogue();
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
}
