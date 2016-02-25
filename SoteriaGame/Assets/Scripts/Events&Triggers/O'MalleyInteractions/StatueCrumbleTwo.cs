using UnityEngine;
using System.Collections;

public class StatueCrumbleTwo : Reaction
{
	public override void execute()
	{
		GameDirector.instance.StatueCrumbleTwo();
	}
}