using UnityEngine;
using System.Collections;

public class PuppetPuzzleIntro : MonoBehaviour
{
	private GameObject _controller;
	public GameObject portToDistrict;

	void Start()
	{
		this._controller = GameObject.Find("PuppetPuzzleController");
		if (!GameDirector.instance.GetPuppetActivated())
		{
			portToDistrict.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			//this._controller.GetComponent<PuppetPuzzleController>().OpenBossEye();
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaEnteringTheaterPuzzFirstTime");
			GameDirector.instance.StartDialogue(true);
			GameDirector.instance.PuppetPuzzleActivated();
			GameDirector.instance.HubPhase3();
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