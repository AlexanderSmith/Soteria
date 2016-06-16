using UnityEngine;
using System.Collections;

public class MusPuzzle3 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.MusicPuzzleVisitedSuit();
		}
	}
}