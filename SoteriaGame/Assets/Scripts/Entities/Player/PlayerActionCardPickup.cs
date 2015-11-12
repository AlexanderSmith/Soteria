using UnityEngine;
using System.Collections;

public class PlayerActionCardPickup : IPlayerAction
{	
	public void PlayerAction(Player inPlayer)
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			// Pick up this card
			GameDirector.instance.EndCardInteraction();
			GameDirector.instance.GetPlayer().PlayerActionNormal();
		}
		else if (Input.GetKeyDown(KeyCode.N))
		{
			// Don't pick up this card
			GameDirector.instance.EndCardInteraction();
			GameDirector.instance.GetPlayer().PlayerActionNormal();
		}
	}
}