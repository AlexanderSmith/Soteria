using UnityEngine;
using System.Collections;

public class LanternRange : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetLanternController().IsCharged() && GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			GameDirector.instance.PulseLantern();
		}
	}
	
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetLanternController().IsCharged() && GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			GameDirector.instance.PulseLantern();
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
