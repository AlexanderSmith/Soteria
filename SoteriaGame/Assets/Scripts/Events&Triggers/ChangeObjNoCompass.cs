using UnityEngine;
using System.Collections;

public class ChangeObjNoCompass : MonoBehaviour
{
	GameObject lanternObjective;
	GameObject portToMusic;
	GameObject ptLights;

	void Start()
	{
		lanternObjective = GameObject.Find ("ReceiveLantern");
		portToMusic = GameObject.Find("HubToMusic");
		ptLights = GameObject.Find ("SoteriaPowerSystem_Lights");
	}
	
	void OnTriggerEnter(Collider player)
	{
		ptLights.SetActive(false);
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetCompass())
		{
			ptLights.SetActive(true);
			if (!GameDirector.instance.GetLantern())
			{
				GameDirector.instance.ChangeObjective(lanternObjective);
			}
			else
			{
				GameDirector.instance.ChangeObjective(portToMusic);
			}
		}
	}
}
