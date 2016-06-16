using UnityEngine;
using System.Collections;

public class PortToHub : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.CheckLantern();
			GameDirector.instance.ClearAudioList();
			Application.LoadLevel("HUBPass1");
		}
	}
}