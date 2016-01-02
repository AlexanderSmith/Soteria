using UnityEngine;
using System.Collections;

public class ChangeObjNoCompass : MonoBehaviour
{
	GameObject lanternObjective;
	GameObject portToMusic;
	GameObject ptLights;
	GameObject dreams;

	void Start()
	{
		lanternObjective = GameObject.Find ("ReceiveLantern");
		portToMusic = GameObject.Find("HubToMusic");
		ptLights = GameObject.Find ("SoteriaPowerSystem_Lights");
		dreams = GameObject.Find("DreamsDirectionDialogue");
	}
	
	void OnTriggerEnter(Collider player)
	{
		ptLights.SetActive(false);
		dreams.SetActive(false);
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
				if (!GameDirector.instance.GetDreams())
				{
					dreams.SetActive(true);
				}
			}
		}
	}
}