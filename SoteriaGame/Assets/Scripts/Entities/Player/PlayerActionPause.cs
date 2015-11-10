using UnityEngine;

public class PlayerActionPause : IPlayerAction
{	
	public void PlayerAction(Player inPlayer)
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameDirector.instance.GetPlayer().PlayerActionNormal();
		}
	}
}