using UnityEngine;
using System.Collections;

public class HubToMusic : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			switch (GameDirector.instance.GetGamePhase())
			{
			case 1:
				Application.LoadLevel("MusicDistrictPass1");
				break;
			case 2:
				Application.LoadLevel("MusicDistrictPass2");
				break;
			case 3:
				Application.LoadLevel("MusicDistrictPass3");
				break;
			case 4:
				Application.LoadLevel ("MusicDistrictPass4");
				break;
			}
		}
	}
}