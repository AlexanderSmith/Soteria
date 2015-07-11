using UnityEngine;

public class PlayerNormal : IPlayerAction
{
	public void PlayerAction(GameObject player)
	{
		player.rigidbody.MovePosition (Vector3.zero);
	}
}