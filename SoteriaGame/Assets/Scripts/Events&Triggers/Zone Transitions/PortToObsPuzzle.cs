using UnityEngine;
using System.Collections;

public class PortToObsPuzzle : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ClearAudioList();
			Application.LoadLevel("ObservatoryPuzzle");
		}
	}
}