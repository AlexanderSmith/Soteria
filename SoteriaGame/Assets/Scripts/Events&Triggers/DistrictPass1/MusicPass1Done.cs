using UnityEngine;
using System.Collections;

public class MusicPass1Done : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.MusicPass1Done();
			GameDirector.instance.FirstTimeMusicPuzzle();
		}
	}
}
