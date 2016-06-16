using UnityEngine;
using System.Collections;

public class ObsPuzzle3 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ObservatoryPuzzleVisitedSuit();
		}
	}
}