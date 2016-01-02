using UnityEngine;
using System.Collections;

public class ChangeObjectiveTheater : MonoBehaviour
{
	GameObject sewer;
	GameObject puppetStore;
	
	void Start()
	{
		sewer = GameObject.Find ("TheaterSewerGate");
		puppetStore = GameObject.Find("PuppetStore");
		GameDirector.instance.ChangeObjective(puppetStore);
	}
	
//	void OnTriggerEnter(Collider player)
//	{
//		if (player.gameObject.tag == "Player" && !GameDirector.instance.GetTheaterPass1())
//		{
//			//if (!GameDirector.instance.GetVisitedSewer())
//			//{
//				//GameDirector.instance.ChangeObjective(sewer);
//			//}
//			//else
//			//{
//				GameDirector.instance.ChangeObjective(puppetStore);
//			//}
//		}
//	}
}