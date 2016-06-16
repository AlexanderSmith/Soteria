using UnityEngine;
using System.Collections;

public class ObservatoryTile : MonoBehaviour
{
	void Awake()
	{
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.HiddenTile);
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Normal);
		}
	}
}
