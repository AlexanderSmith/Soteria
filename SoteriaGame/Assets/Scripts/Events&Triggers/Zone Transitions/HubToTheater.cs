using UnityEngine;
using System.Collections;

public class HubToTheater : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			switch (GameDirector.instance.GetGamePhase())
			{
			case 1:
				Application.LoadLevel("TheaterPass1");
				break;
			case 2:
				Application.LoadLevel("TheaterPass2");
				break;
			case 3:
				Application.LoadLevel("TheaterPass3");
				break;
			case 4:
				Application.LoadLevel ("TheaterPass4");
				break;
			}
		}
	}
}