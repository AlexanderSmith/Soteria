using UnityEngine;
using System.Collections;

public class StatueCrumbleThree : Reaction
{
	public override void execute()
	{
		GameDirector.instance.StatueCrumbleThree();
	}
}