using UnityEngine;
using System.Collections;

public class ChangeObjectiveObservatory : MonoBehaviour
{
	GameObject sewer;
	GameObject puzzle;
	
	void Start()
	{
		sewer = GameObject.Find ("SewerGate");
		puzzle = GameObject.Find("PortToHubFromPuzzle");
	}
	
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && !GameDirector.instance.GetObservatoryPass1())
		{
			//if (!GameDirector.instance.GetVisitedSewer())
			//{
				//GameDirector.instance.ChangeObjective(sewer);
			//}
			//else
			//{
				GameDirector.instance.ChangeObjective(puzzle);
			//}
		}
	}
}