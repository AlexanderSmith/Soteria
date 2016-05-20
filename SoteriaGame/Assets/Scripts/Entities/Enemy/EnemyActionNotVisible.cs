using UnityEngine;
using System.Collections;

public class EnemyActionNotVisible : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.Unaware();
	}
}