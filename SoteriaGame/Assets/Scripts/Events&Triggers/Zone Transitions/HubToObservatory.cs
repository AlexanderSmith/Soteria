using UnityEngine;
using System.Collections;

public class HubToObservatory : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		GameDirector.instance.ClearAudioList();
		if (player.gameObject.tag == "Player")
		{
			switch (GameDirector.instance.GetGamePhase())
			{
			case 1:
				Application.LoadLevel("ObservatoryPass1");
				break;
			case 2:
				Application.LoadLevel("ObservatoryPass2");
				break;
			case 3:
				Application.LoadLevel("ObservatoryPass3");
				break;
			case 4:
				Application.LoadLevel ("ObservatoryPass4");
				break;
			}
		}
	}
}