using UnityEngine;
using System.Collections;

public class ObsPuzzleToDistrict : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ClearAudioList();
			GameDirector.instance.SetFromPuzzle();
			Application.LoadLevel("ObservatoryPass1");
			//			switch (GameDirector.instance.GetGamePhase())
			//			{
			//			case 1:
			//				Application.LoadLevel("ObservatoryPass1");
			//				break;
			//			case 2:
			//				Application.LoadLevel("ObservatoryPass2");
			//				break;
			//			case 3:
			//				Application.LoadLevel("ObservatoryPass3");
			//				break;
			//			case 4:
			//				Application.LoadLevel ("ObservatoryPass4");
			//				break;
			//			}
		}
	}
}
