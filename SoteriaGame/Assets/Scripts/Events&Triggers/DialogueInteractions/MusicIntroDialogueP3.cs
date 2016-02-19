using UnityEngine;
using System.Collections;

public class MusicIntroDialogueP3 : MonoBehaviour
{
	private GameObject _introDiag;

	void Start()
	{
		_introDiag = GameObject.Find("MusicSuitIntro");
		if (GameDirector.instance.GetMusicSuitIntro())
		{
			_introDiag.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.MusicSuitIntroDone();
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaDistEnterVOMUSICp3");
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
			}
		}
	}
}