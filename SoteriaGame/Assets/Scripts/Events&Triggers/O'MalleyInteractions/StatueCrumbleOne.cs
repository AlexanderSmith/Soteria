using UnityEngine;
using System.Collections;

public class StatueCrumbleOne : Reaction
{
	public override void execute()
	{
		GameDirector.instance.StatueCrumbleOne();
		if (GameDirector.instance.GetCompass())
		{
			GameDirector.instance.ChangeObjective(GameObject.Find ("PortToHub"));
		}
	}
}