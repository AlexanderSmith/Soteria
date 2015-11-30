using UnityEngine;
using System.Collections;

public class GetSuit : Reaction
{
	public override void execute()
	{
		this.GetComponent<SphereCollider>().enabled = false;
		GameDirector.instance.SuitWorn();
	}
}