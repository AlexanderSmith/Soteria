using UnityEngine;
using System.Collections;

public class PortToMusicPuzzle : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ClearAudioList();
			Application.LoadLevel("MusicPuzzle");
		}
	}
}
