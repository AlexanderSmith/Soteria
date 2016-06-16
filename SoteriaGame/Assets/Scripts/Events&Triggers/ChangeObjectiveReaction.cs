using UnityEngine;
using System.Collections;

public class ChangeObjectiveReaction : Reaction
{
	public override void execute ()
	{
		GameDirector.instance.ChangeObjective(null);
	}
}