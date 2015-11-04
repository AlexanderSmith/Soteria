using UnityEngine;

public class PlayerActionPause : IPlayerAction
{	
	public void PlayerAction(Player inPlayer)
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameDirector.instance.EndDialogue();
			GameDirector.instance.GetPlayer().PlayerActionNormal();
		}
	}

	public void InitializeValues(Player player)
	{}
}