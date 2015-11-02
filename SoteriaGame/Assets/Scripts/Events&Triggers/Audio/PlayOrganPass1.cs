using UnityEngine;
using System.Collections;

public class PlayOrganPass1 : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.PlayAudioClip(AudioID.OrganMusic);
		}
	}
}
