using UnityEngine;
using System.Collections;

public class HubPhase4 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.HubPhase4();
		}
	}
}