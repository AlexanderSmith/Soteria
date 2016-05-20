using UnityEngine;
using System.Collections;

public class EnemyActionStunned : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.Stunned();
	}
}