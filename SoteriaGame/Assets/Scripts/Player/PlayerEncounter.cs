using UnityEngine;

public class PlayerEncounter : IPlayerAction
{
	public void PlayerAction(GameObject player)
	{
		player.rigidbody.MovePosition (Vector3.zero);
	}
}