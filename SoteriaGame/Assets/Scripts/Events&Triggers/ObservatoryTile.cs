using UnityEngine;
using System.Collections;

public class ObservatoryTile : MonoBehaviour
{
	void Awake()
	{
		this.GetComponentInChildren<Light>().color = Color.cyan;
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
			this.GetComponentInChildren<Light>().color = Color.white;
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Normal);
			this.GetComponentInChildren<Light>().color = Color.cyan;
		}
	}
}
