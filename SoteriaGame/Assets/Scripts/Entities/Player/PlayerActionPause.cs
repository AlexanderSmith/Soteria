using UnityEngine;

public class PlayerActionPause : IPlayerAction
{	
	public void PlayerAction(Player inPlayer)
	{
//		if (Input.GetKeyDown(KeyCode.Escape))
//		{
//			GameDirector.instance.GetPlayer().PlayerActionNormal();
//		}
		if (Input.GetKeyDown (KeyCode.End))
		{
			GameDirector.instance.SkipLine();
		}
	}

	public void InitializeValues(Player inPlayer)
	{
	}
}