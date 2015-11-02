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
				GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 0.1f);
				controller.GetComponent<MusicPuzzleController>().PuzzleActivated();
			}
		}
	}
}
