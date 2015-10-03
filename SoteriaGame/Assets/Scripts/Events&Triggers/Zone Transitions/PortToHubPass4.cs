using UnityEngine;
using System.Collections;

public class PortToHubPass4 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("HUBPass4");
			GameDirector.instance.HubPhase4();
		}
	}
}
