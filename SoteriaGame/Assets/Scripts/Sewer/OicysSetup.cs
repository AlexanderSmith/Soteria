using UnityEngine;
using System.Collections;

public class OicysSetup : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this.GetComponentInChildren<SewerController>().StartEncounter();
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}