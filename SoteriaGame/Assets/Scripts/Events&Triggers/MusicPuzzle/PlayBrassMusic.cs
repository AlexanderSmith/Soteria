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
			GameDirector.instance.ChangeVolume(AudioID.BrassMusic, 0.1f);
			controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().MusicStart(AudioID.BrassMusic, "brass");
			Destroy(this.GetComponent<BoxCollider>());
		}
	}
}
