using UnityEngine;
using System.Collections;

public class PortToHub : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			switch (GameDirector.instance.GetHubPhase())
			{
			case 5:
				Application.LoadLevel("HUBPass3");
				break;
			case 1:
				Application.LoadLevel("HUBPass1");
				break;
			case 2:
				Application.LoadLevel("HUBPass2");
				break;
			case 3:
				Application.LoadLevel("HUBPass3");
				break;
			case 4:
				Application.LoadLevel("HUBPass4");
				break;
			}
			// If player has lantern, recharge lantern on return to hub
			GameDirector.instance.CheckLantern();
		}
	}
}
