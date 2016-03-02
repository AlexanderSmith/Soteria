using UnityEngine;
using System.Collections;

public class PlayerActionOicysEnc : IPlayerAction
{	
	private int _keyPressCounter;
	private int _lingerLonger;
	private bool _lingering = false;
	
	public void PlayerAction(Player inPlayer)
	{
		this.FightOicys();
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
				GameDirector.instance.PlayerOvercameOicys();
				Debug.Log("player wins");
			}
		}
	}
	
	public void InitializeValues(Player inPlayer)
	{
		if (!inPlayer.encounterVariables)
		{
			this._keyPressCounter = 0;
			_lingering = false;
			inPlayer.FlipEncounterBool();
		}
	}
	
	private void FightOicys()
	{
		GameDirector.instance.Overpower();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!_lingering)
			{
				_lingering = true;
				GameDirector.instance.BeginLingering();
			}
			this._keyPressCounter++;
			GameDirector.instance.EncounterClear();
		}
	}