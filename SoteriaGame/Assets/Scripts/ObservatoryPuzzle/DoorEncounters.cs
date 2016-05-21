using UnityEngine;
using System.Collections;

public class DoorEncounters : MonoBehaviour
{
	public GameObject _thisEncounter;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
//			GameDirector.instance.ObsPuzzleEncounter();
//			this.GetComponent<BoxCollider>().enabled = false;
			ObsPuzzleWhisperSetup();
			GameDirector.instance.ChangeVolume(AudioID.Whispers, 0.5f);
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player" && !GameDirector.instance.isDialogueActive())
		{
			GameDirector.instance.ObsPuzzleEncounter();
			this.GetComponent<BoxCollider>().enabled = false;
		}
	}

	void ObsPuzzleWhisperSetup()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();

		switch (this._thisEncounter.name)
		{
		case "DoorEncounter1":
			GameDirector.instance.SetupDialogue("AnaObservPuzzFirstLinger");
			break;
		case "DoorEncounter2":
			GameDirector.instance.SetupDialogue("AnaObservPuzzSecondLinger");
			break;
		case "DoorEncounter3":
			GameDirector.instance.SetupDialogue("AnaObservPuzzThirdLinger");
			break;
		};

		GameDirector.instance.StartDialogue();
	}
}