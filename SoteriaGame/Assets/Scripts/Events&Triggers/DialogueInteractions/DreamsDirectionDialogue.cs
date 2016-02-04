using UnityEngine;
using System.Collections;

public class DreamsDirectionDialogue : MonoBehaviour
{
	private GameObject _hubToMusic;

	void Start()
	{
		_hubToMusic = GameObject.Find("HubToMusic");
		_hubToMusic.SetActive(false);
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.DreamsTrue();
			GameDirector.instance.SetupDialogue("AnaHubBeforeEnteringDistrict");
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
				_hubToMusic.SetActive(true);
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}