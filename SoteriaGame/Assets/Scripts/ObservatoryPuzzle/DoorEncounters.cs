using UnityEngine;
using System.Collections;

public class DoorEncounters : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ObsPuzzleEncounter();
			this.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
