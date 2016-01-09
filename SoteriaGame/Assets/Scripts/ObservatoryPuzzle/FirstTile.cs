using UnityEngine;
using System.Collections;

public class FirstTile : MonoBehaviour
{

	public GameObject tileCtr;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			tileCtr.GetComponentInChildren<Light>().enabled = true;
		}
	}
}
