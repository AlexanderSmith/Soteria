using UnityEngine;
using System.Collections;

public class IntroDialogue : MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("IntroDialogue", AudioID.None);
		GameDirector.instance.StartDialogue();
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
}
