using UnityEngine;
using System.Collections;

public class HubPhase5 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.HubPhase5();
		}
	}
}