using UnityEngine;
using System.Collections;

public class PuppetPuzzleToDistrict : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ClearAudioList();
			GameDirector.instance.SetFromPuzzle();
			Application.LoadLevel("TheaterPass1");
			//			switch (GameDirector.instance.GetGamePhase())
			//			{
			//			case 1:
			//				Application.LoadLevel("TheaterPass1");
			//				break;
			//			case 2:
			//				Application.LoadLevel("TheaterPass2");
			//				break;
			//			case 3:
			//				Application.LoadLevel("TheaterPass3");
			//				break;
			//			case 4:
			//				Application.LoadLevel ("TheaterPass4");
			//				break;
			//			}
		}
	}
}