﻿using UnityEngine;
using System.Collections;

public class PuppetPuzzleIntro : MonoBehaviour
{
	private GameObject _controller;

	void Start()
	{
		this._controller = GameObject.Find("PuppetPuzzleController");
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaEnteringTheaterPuzzFirstTime");
			GameDirector.instance.StartDialogue();
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				this._controller.GetComponent<PuppetPuzzleController>().LeftLightOn();
			}
		}
	}
}