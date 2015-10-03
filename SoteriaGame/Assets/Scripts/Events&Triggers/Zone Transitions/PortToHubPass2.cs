using UnityEngine;
using System.Collections;

public class PortToHubPass2 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("HUBPass2");
			GameDirector.instance.HubPhase2();
		}
	}
}
