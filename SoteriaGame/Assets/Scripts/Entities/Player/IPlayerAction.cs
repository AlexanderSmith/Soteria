using UnityEngine;

public interface IPlayerAction
{
	void PlayerAction(Player player);

	void InitializeValues(Player player);
}