using UnityEngine;
using System.Collections;

public class InspectFortuneTeller : InteractionBase
{
	public void TurnOffInspect()
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		this.gameObject.GetComponentInParent<BoxCollider>().enabled = false;
	}

	public override void TriggerExit(Collider player)
	{
	}
	
	public override void TriggerStay(Collider player)
	{
	}	
}
