using UnityEngine;
using System.Collections;

public class HubToObservatory : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("WalkThroughObservatory");
		}
	}
}