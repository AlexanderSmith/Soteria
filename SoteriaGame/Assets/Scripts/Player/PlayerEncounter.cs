using UnityEngine;

public class PlayerEncounter : IPlayerAction
{
	public void PlayerAction(Player player)
	{
		player.rigidbody.MovePosition (Vector3.zero);
	}
}