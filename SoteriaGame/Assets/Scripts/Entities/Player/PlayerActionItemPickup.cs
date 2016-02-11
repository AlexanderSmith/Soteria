using UnityEngine;
using System.Collections;

public class PlayerActionItemPickup : IPlayerAction
{	
	public void PlayerAction(Player inPlayer)
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// Pick up this card
			GameDirector.instance.EndItemInteraction(true);
			GameDirector.instance.GetPlayer().PlayerActionNormal();
		}
	}
	
	public void InitializeValues(Player inPlayer)
	{
	}
}