using UnityEngine;
using System.Collections;

public class ObservatoryTile : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
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
