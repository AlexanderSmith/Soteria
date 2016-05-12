using UnityEngine;
using System.Collections;

public class EnemyActionTheater : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.distance = Vector3.Distance(inController.transform.position, GameDirector.instance.GetPlayer().transform.position);

		if (inController.distance <= inController.overwhelmRange)
		{
			GameDirector.instance.Encounter(inController.gameObject);
			inController.OverwhelmPlayer();
		}
		else
		{
			inController._agent.SetDestination(GameDirector.instance.GetPlayer().gameObject.transform.position);
		}
	}
}