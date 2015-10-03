using UnityEngine;
using System.Collections;

public class PortToHubPass3 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("HUBPass3");
			GameDirector.instance.HubPhase3();
		}
	}
}
