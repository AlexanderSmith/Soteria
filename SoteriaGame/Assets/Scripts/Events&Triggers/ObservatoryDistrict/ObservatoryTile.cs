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

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			switch (GameDirector.instance.GetGameState ())
			{
			case GameStates.Normal:
				GameDirector.instance.ChangeGameState(GameStates.HiddenTile);
				break;
			default:
				break;
			}
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