using UnityEngine;
using System.Collections;

public class KillMusic : MonoBehaviour
{
	void OnTriggerStay (Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 0.1f);
				GameDirector.instance.ChangeVolume(AudioID.BrassMusic, 0.1f);
				GameDirector.instance.ChangeVolume(AudioID.WindMusic, 0.1f);
				GameDirector.instance.ChangeVolume(AudioID.StringMusic, 0.1f);
			}
		}
	}
}
