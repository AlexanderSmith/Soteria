using UnityEngine;
using System.Collections;

public class PortToHubFromObservatory : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.CheckLantern();
			GameDirector.instance.ClearAudioList();
			GameDirector.instance.SetFromObservatoryDistrict();
			switch (GameDirector.instance.GetHubPhase())
			{
			case 5:
				Application.LoadLevel("HUBPass3");
				break;
			case 1:
				Application.LoadLevel("HUBPass1");
				break;
			case 2:
				Application.LoadLevel("HUBPass2");
				break;
			case 3:
				Application.LoadLevel("HUBPass3");
				break;
			case 4:
				Application.LoadLevel("HUBPass4");
				break;
			}
		}
	}
}
