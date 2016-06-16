using UnityEngine;
using System.Collections;

public class HubToMusic : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		GameDirector.instance.ClearAudioList();
		if (player.gameObject.tag == "Player")
		{
			switch (GameDirector.instance.GetGamePhase())
			{
			case 1:
				Application.LoadLevel("MusicPass1");
				break;
			case 2:
				Application.LoadLevel("MusicPass2");
				break;
			case 3:
				Application.LoadLevel("MusicPass3");
				break;
			case 4:
				Application.LoadLevel ("MusicPass4");
				break;
			}
		}
	}
}