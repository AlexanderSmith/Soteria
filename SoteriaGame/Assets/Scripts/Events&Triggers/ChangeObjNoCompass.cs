using UnityEngine;
using System.Collections;

public class ChangeObjNoCompass : MonoBehaviour
{
	GameObject lanternObjective;
	GameObject portToMusic;
	GameObject ptLights;
	GameObject dreams;
	public GameObject lantern;

	void Start()
	{
		lanternObjective = GameObject.Find ("ReceiveLantern");
		portToMusic = GameObject.Find("HubToMusic");
		//ptLights = GameObject.Find ("SoteriaPowerSystem_Lights");
		dreams = GameObject.Find("DreamsDirectionDialogue");

		//ptLights.SetActive(false);
		lantern.GetComponent<GetLantern> ().SetDreams (dreams);
		dreams.SetActive(false);
		if (GameDirector.instance.GetCompass())
		{
			//ptLights.SetActive(true);
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

		lantern.GetComponent<GetLantern> ().SetHubToMusic (portToMusic);
	}
}