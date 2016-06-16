using UnityEngine;
using System.Collections;

public class TileLight : MonoBehaviour
{
	void Awake()
	{
		this.GetComponentInChildren<Light>().color = Color.cyan;
	}
	
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this.GetComponentInChildren<Light>().color = Color.white;
		}
	}
	
	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this.GetComponentInChildren<Light>().color = Color.cyan;
		}
	}
}