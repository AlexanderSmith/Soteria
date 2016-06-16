using UnityEngine;
using System.Collections;

public class MusicPuzzleToDistrict : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ClearAudioList();
			GameDirector.instance.SetFromPuzzle();
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
				Application.LoadLevel("MusicPass4");
				break;
			}
		}
	}
}