using UnityEngine;
using System.Collections;

public class SwarmSpawner : MonoBehaviour
{
	private GameObject swarm;

	void Start()
	{
		swarm = GameObject.FindWithTag("Swarm");
		swarm.SetActive(false);
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			swarm.SetActive(true);
		}
	}
}
