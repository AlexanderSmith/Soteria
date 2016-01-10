using UnityEngine;
using System.Collections;

public class TriggerAllLights : MonoBehaviour
{
	private GameObject[] _lights;

	void Start()
	{
		this._lights = GameObject.FindGameObjectsWithTag("TileLight");
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			foreach (GameObject light in _lights)
			{
				light.GetComponentInChildren<Light>().enabled = true;
				light.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
}
