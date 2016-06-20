using UnityEngine;
using System.Collections;

public class EnemyActionHidden : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.Unaware();
	}
}
