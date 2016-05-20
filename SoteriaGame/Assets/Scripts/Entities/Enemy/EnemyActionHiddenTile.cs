using UnityEngine;
using System.Collections;

public class EnemyActionHiddenTile : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.LookAtPlayer();
		GameDirector.instance.PlayerOnObservatoryTile();
	}
}
