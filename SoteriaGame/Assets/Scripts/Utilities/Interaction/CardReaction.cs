using UnityEngine;
using System.Collections;

public class CardReaction : Reaction
{
	public override void execute ()
	{
		GameDirector.instance.GetPlayer().PlayerActionCardPickup();
		GameDirector.instance.EnableCardResponseOptions();
	}
}