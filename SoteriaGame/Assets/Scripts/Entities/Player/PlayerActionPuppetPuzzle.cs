using UnityEngine;
using System.Collections;

public class PlayerActionPuppetPuzzle : IPlayerAction
{
	private int _keyPressCounter;
	private GameObject _controller;
	private bool _lingering;

	public void PlayerAction(Player inPlayer)
	{
		this.FightPuppetPuzzle();
		if (this._keyPressCounter == 10 & !GameDirector.instance.GetOvercomeBool())
		{
			this._keyPressCounter = 0;
			GameDirector.instance.TryingToOvercome();
		}
		else if (GameDirector.instance.GetOvercomeBool())
		{
			GameDirector.instance.AbleToOvercome();
			if (this._keyPressCounter > 10)
			{
				GameDirector.instance.PuzzleOvercome();

				Debug.Log("player wins");
			}
		}
	}
	
	public void InitializeValues(Player inPlayer)
	{
		if (!inPlayer.encounterVariables)
		{
			inPlayer.FlipEncounterBool();
			this._keyPressCounter = 0;
			this._lingering = false;
			this._controller = GameObject.Find("PuppetPuzzleController");
		}
	}
	
	void FightPuppetPuzzle()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!_lingering)
			{
				_lingering = true;
				GameDirector.instance.BeginLingering();
			}
			this._keyPressCounter++;
			GameDirector.instance.PuppetPuzzleEncounterClear();
		}
	}
}