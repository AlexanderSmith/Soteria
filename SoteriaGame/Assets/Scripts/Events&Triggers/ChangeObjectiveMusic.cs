using UnityEngine;
using System.Collections;

public class ChangeObjectiveMusic : MonoBehaviour
{
	GameObject sewer;
	GameObject musicStore;
	
	void Start()
	{
		sewer = GameObject.Find ("SewerEntrance");
		musicStore = GameObject.Find("MusicStore");
	}
	
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && !GameDirector.instance.GetMusicPass1())
		{
			if (!GameDirector.instance.GetVisitedSewer())
			{
				GameDirector.instance.ChangeObjective(sewer);
			}
			else
			{
				GameDirector.instance.ChangeObjective(musicStore);
			}
		}
	}
}