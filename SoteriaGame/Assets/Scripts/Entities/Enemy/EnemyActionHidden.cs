using UnityEngine;
using System.Collections;

public class EnemyActionHidden : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.distance = Vector3.Distance(inController.transform.position, GameDirector.instance.GetPlayer().transform.position);

		if (inController.distance <= inController.overwhelmRange)
		{
			inController.LookAtPlayer();
		}
		else if (inController.distance <= inController.attackRange)
		{
			inController.ChasePlayer();
		}
		else if (inController.distance <= inController.lookAtDistance)
		{
			inController.LookAtPlayer();
		}
		else
		{
			inController.Unaware();
		}
	}
}
