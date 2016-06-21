using UnityEngine;
using System.Collections;

public class EnemyActionInteraction : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController._agent.Stop();
	}
}