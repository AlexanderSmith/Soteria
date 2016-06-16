using UnityEngine;
using System.Collections;

public class PlayOrganMusic : MonoBehaviour
{
	private GameObject controller;

	void Start()
	{
		controller = GameObject.Find("MusicPuzzleControl");
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.GetMusicActivated())
			{
				GameDirector.instance.PlayAudioClip(AudioID.OrganMusicComplete);
				controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().HackStart(AudioID.OrganMusicComplete);
				GameDirector.instance.GetPlayer().PlayerActionPause();
				GameDirector.instance.SetupDialogue("WhispersMusicPuzzleActivation");
				GameDirector.instance.StartDialogue();
			}
			else
			{
				GameDirector.instance.GetPlayer().PlayerActionPause();
				GameDirector.instance.SetupDialogue("MusicPuzzFourthLingerResponse");
				GameDirector.instance.StartDialogue();
			}
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive() && !GameDirector.instance.GetMusicActivated())
			{
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				//GameDirector.instance.PlayAudioClip(AudioID.OrganMusicComplete);
				//GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 1f);
				controller.GetComponent<MusicPuzzleController>().PuzzleActivated();
				controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().PuzzleStart();
			}
			else if (!GameDirector.instance.isDialogueActive() && !controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().GetHackStart())
			{
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				GameDirector.instance.ChangeVolume(AudioID.OrganMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
				controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.OrganMusic, "Organ");
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
}