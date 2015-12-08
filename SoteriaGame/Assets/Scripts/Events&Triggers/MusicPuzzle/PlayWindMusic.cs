using UnityEngine;
using System.Collections;

public class PlayWindMusic : MonoBehaviour
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
            GameDirector.instance.ChangeVolume(AudioID.WindMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
			controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.WindMusic, "Wind");
			Destroy(this.GetComponent<BoxCollider>());
		}
	}
}
