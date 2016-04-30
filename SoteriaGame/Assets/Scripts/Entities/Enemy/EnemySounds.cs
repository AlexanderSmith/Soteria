using UnityEngine;
using System.Collections;

public class EnemySounds : MonoBehaviour
{
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			float currentDist = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);
			if (currentDist <= 0)
			{
				currentDist = .01f;
			}
			GameDirector.instance.ChangeVolume(AudioID.Whispers, 1 - (currentDist / 45.0f));
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeVolume(AudioID.Whispers, 0);
		}
	}
}