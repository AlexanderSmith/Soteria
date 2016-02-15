using UnityEngine;
using System.Collections;

public class PlayBrassMusic : MonoBehaviour
{
	GameObject controller;
	
	void Start()
	{
		controller = GameObject.Find("MusicPuzzleControl");
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("MusicPuzzFirstLingerResponse");
			GameDirector.instance.StartDialogue();
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				GameDirector.instance.ChangeVolume(AudioID.BrassMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
				controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.BrassMusic, "Brass");
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
}