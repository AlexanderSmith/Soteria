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
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeVolume(AudioID.BrassMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
			controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.BrassMusic, "Brass");
			Destroy(this.GetComponent<BoxCollider>());
		}
	}
}
