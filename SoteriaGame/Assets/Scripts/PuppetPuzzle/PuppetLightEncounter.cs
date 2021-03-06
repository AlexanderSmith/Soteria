﻿using UnityEngine;
using System.Collections;

public class PuppetLightEncounter : MonoBehaviour
{
	private GameObject _controller;
	public GameObject _thisLight;
	public GameObject _nextLight;
	private bool _active;
	private bool _defeated;

	void Start()
	{
		this._controller = GameObject.Find("PuppetPuzzleController");
		this._active = false;
		this._defeated = false;
	}

	void Update()
	{
		if (this._active)
		{
			this.transform.position = new Vector3(GameDirector.instance.GetPlayer().transform.position.x, this.transform.position.y,
			                                      GameDirector.instance.GetPlayer().transform.position.z);
			if (!GameDirector.instance.isDialogueActive() && !this._defeated)
			{
				//GameDirector.instance.GetPlayer().PlayerActionNormal();
				GameDirector.instance.PuppetPuzzleEncounter();
				this._controller.GetComponent<PuppetPuzzleController>().LightEncounter(_thisLight, _nextLight);
//				this.GetComponent<SphereCollider>().enabled = false;
			}
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			this.GetComponent<SphereCollider>().enabled = false;
			WhisperDialogueSetup(_thisLight);
			this._active = true;
			GameDirector.instance.ChangeVolume(AudioID.Whispers, 0.5f);
//			GameDirector.instance.PuppetPuzzleEncounter();
//			this._controller.GetComponent<PuppetPuzzleController>().LightEncounter(_nextLight);
//			this.GetComponent<SphereCollider>().enabled = false;
		}
	}

	void WhisperDialogueSetup(GameObject light)
	{
		GameDirector.instance.PuppetPuzzleWhiteOut();

		switch(light.name)
		{
		case "LeftSpot":
			LeftSpotDialogue();
			break;
		case "BackSpot":
			BackSpotDialogue();
			break;
		case "RightSpot":
			RightSpotDialogue();
			break;
		case "FinalSpot":
			FinalSpotDialogue();
			break;
		};
	}

	void LeftSpotDialogue()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("WhispersPuppetPuzzleActivation");
		GameDirector.instance.StartDialogue();
//		GameDirector.instance.PuppetPuzzleEncounter();
//		this._controller.GetComponent<PuppetPuzzleController>().LightEncounter(_nextLight);
	}

	void BackSpotDialogue()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("AnaTheaterPuzzFirstLinger");
		GameDirector.instance.StartDialogue();
	}

	void RightSpotDialogue()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("AnaTheaterPuzzSecondLinger");
		GameDirector.instance.StartDialogue();
	}

	void FinalSpotDialogue()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("AnaTheaterPuzzThirdLinger");
		GameDirector.instance.StartDialogue();
	}

	public void LightDefeated()
	{
		this._defeated = true;
		GameDirector.instance.ChangeVolume(AudioID.Whispers, 0);
		GameDirector.instance.NewWhiteOut();
	}
}