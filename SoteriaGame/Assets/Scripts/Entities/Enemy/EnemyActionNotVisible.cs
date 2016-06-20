using UnityEngine;
using System.Collections;

public class EnemyActionNotVisible : IEnemyAction
{
	public void EnemyAction(BasicEnemyController inController)
	{
		inController.distance = Vector3.Distance(inController.transform.position, GameDirector.instance.GetPlayer().transform.position);
		if (inController.distance > inController.lookAtDistance)
		{
			inController.Unaware();
		}
		else
		{			
			Vector3 direction = GameDirector.instance.GetPlayer().transform.position - inController.gameObject.transform.position;
			float angle = Vector3.Angle(direction, inController.gameObject.transform.forward);
			
			if(angle < inController.fieldOfVision * 0.5f)
			{
				RaycastHit hit;
				
				if(Physics.Raycast(inController.gameObject.transform.position + (inController.eyeHeightOffset * inController.gameObject.transform.up), direction, out hit, inController.sphereCollider.radius))
				{
					if(hit.collider.gameObject.Equals(GameDirector.instance.GetPlayer().gameObject))
					{
						inController.GetComponent<BasicEnemyController>().VisibleAction();
					}
				}
				//Debug.DrawRay(this.gameObject.transform.position + (eyeHeightOffset * this.gameObject.transform.up), direction, Color.white, 200, false);
			}
			else
			{
				inController.Unaware();
			}
		}
	}
}