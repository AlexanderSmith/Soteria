﻿using UnityEngine;
using System.Collections;

public class PlayStringMusic : MonoBehaviour
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
			GameDirector.instance.SetupDialogue("MusicPuzzSecondLingerResponse");
			GameDirector.instance.StartDialogue();
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				GameDirector.instance.ChangeVolume(AudioID.StringMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
				controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.StringMusic, "String");
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
}