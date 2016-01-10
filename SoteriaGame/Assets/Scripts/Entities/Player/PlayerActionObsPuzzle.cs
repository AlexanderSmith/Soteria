using UnityEngine;
using System.Collections;

public class PlayerActionObsPuzzle : IPlayerAction
{
	private GameObject _controller;

	public void PlayerAction(Player inPlayer)
	{

	}
	
	public void InitializeValues(Player inPlayer)
	{
		if (!inPlayer.encounterVariables)
		{
			inPlayer.FlipEncounterBool();
			this._controller = GameObject.Find("ObsPuzzleController");
		}
	}
}
