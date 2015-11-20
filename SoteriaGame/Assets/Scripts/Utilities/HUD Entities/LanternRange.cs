using UnityEngine;
using System.Collections;

public class LanternRange : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() == GameStates.Normal)
		{
			GameDirector.instance.PulseLantern();
		}
		else
		{
			GameDirector.instance.IdleLantern();
		}
	}
	
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() == GameStates.Normal)
		{
			GameDirector.instance.PulseLantern();
		}
		else
		{
			GameDirector.instance.IdleLantern();
		}
	}
	
	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.IdleLantern();
		}
	}
}
