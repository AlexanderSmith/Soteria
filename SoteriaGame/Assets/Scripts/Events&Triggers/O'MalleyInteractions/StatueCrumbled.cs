using UnityEngine;
using System.Collections;

public class StatueCrumbled : Reaction
{
	public override void execute()
	{
		GameDirector.instance.StatueCrumbled();
	}
}