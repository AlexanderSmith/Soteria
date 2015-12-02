using UnityEngine;
using System.Collections;

public class GetSuit : Reaction
{
	public override void execute()
	{
		GameDirector.instance.SuitWorn();
		GameDirector.instance.ChangeObjective(GameObject.Find("HubToMusic"));
		GameDirector.instance.HubPhase5();
		GameDirector.instance.AddGamePhase();
	}
}