using UnityEngine;
using System.Collections;

public class ChangeObjNoCompass : MonoBehaviour
{
	GameObject lanternObjective;
	GameObject portToMusic;

	void Start()
	{
		lanternObjective = GameObject.Find ("ReceiveLantern");
		portToMusic = GameObject.Find("HubToMusic");
	}
	
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetCompass())
		{
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
