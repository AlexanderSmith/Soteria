using UnityEngine;
using System.Collections;

public class PlayOrganMusic : MonoBehaviour
{
	GameObject controller;

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
				//GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 1f);
				controller.GetComponent<MusicPuzzleController>().PuzzleActivated();
			}
			else
			{
                GameDirector.instance.ChangeVolume(AudioID.OrganMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
				controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.OrganMusic, "Organ");
			}
		}
	}
}
