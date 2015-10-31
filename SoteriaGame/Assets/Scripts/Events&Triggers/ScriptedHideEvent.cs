using UnityEngine;
using System.Collections;

public class ScriptedHideEvent : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (Player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();

		}
	}
}
