using UnityEngine;
using System.Collections;

public class DoorEncounters : MonoBehaviour
{
	public GameObject _thisEncounter;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ObsPuzzleEncounter();
			this.GetComponent<BoxCollider>().enabled = false;
			//ObsPuzzleWhisperSetup();
		}
	}

//	void OnTriggerStay(Collider player)
//	{
//		if (player.gameObject.tag == "Player")
//		{
//			GameDirector.instance.ObsPuzzleEncounter();
//			this.GetComponent<BoxCollider>().enabled = false;
//		}
//	}

	void ObsPuzzleWhisperSetup()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("WhispersMusicPuzzleActivation");
		GameDirector.instance.StartDialogue();
	}
}