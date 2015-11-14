using UnityEngine;
using System.Collections;

public class PlayStringMusic : MonoBehaviour
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
            GameDirector.instance.ChangeVolume(AudioID.StringMusic, controller.GetComponent<MusicPuzzleController>().GetInitialVolume());
			controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.StringMusic, "string");
			Destroy(this.GetComponent<BoxCollider>());
		}
	}
}
