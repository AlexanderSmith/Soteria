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
			GameDirector.instance.PlayAudioClip(AudioID.WindMusic);
		}
	}
}
