using UnityEngine;
using System.Collections;

public class SuitOff : Reaction
{
	public override void execute()
	{
		GameDirector.instance.SuitRemoved();
		GameDirector.instance.AddGamePhase();
		GameDirector.instance.ChangeObjective(GameObject.Find ("HubToMusic"));
	}
}