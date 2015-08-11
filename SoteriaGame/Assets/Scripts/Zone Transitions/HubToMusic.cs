using UnityEngine;
using System.Collections;

public class HubToMusic : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("WalkThroughMusic");
		}
	}
}