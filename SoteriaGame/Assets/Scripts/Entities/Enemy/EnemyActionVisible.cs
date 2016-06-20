using UnityEngine;
using System.Collections;

public class EnemyActionVisible : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.distance = Vector3.Distance(inController.transform.position, GameDirector.instance.GetPlayer().transform.position);
		
		if (inController.distance <= inController.overwhelmRange)
		{
			inController.OverwhelmPlayer();
			GameDirector.instance.Encounter(inController.gameObject);
		}
		else if (inController.distance <= inController.attackRange)
		{
			inController.ChasePlayer();
		}
		else
		{
			inController.LookAtPlayer();
		}
	}
}