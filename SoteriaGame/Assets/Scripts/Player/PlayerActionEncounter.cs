using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActionEncounter : IPlayerAction
{
	private Timer _encounterTimer;
	private TimersType _timerType = TimersType.Encounter;
	private bool _isqtemode;
	private bool _preLinger;

	private int _keyPressCounter;
	public float intialLinger;
	public float baseDuration;
	public float lingerDuration;
	private int _lingerLonger;

	public void PlayerAction(Player inPlayer)
	{
		this.InitializeValues(inPlayer);
		this.ProcessInput();
		if (_preLinger)
		{
			this.LingerTimer();
		}
	}

	public void InitializeValues(Player inPlayer)
	{
		if (!inPlayer.encounterVariables)
		{
			this._preLinger = false;
			this._keyPressCounter = 0;
			this.intialLinger = 1.0f;
			this.baseDuration = 2.0f;
			this.lingerDuration = 2.0f;
			this._lingerLonger = 0;
			this.InitializeTimer();
			inPlayer.FlipEncounterBool();
		}
	}

	private void InitializeTimer()
	{
		this._encounterTimer = TimerManager.instance.Attach(this._timerType);
		this._encounterTimer.StartTimer();
	}

	private void ProcessInput()
	{
		//*/ Single mash turning****************************************************************************************
		if (Input.GetKeyDown(KeyCode.DownArrow) && !GameDirector.instance.GetOvercomeBool())
		{
			GameDirector.instance.TryingToOvercome();
			this._preLinger = true;
			GameDirector.instance.BeginLingering();
			GameDirector.instance.Overpower();
			this._encounterTimer.ResetTimer();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && _preLinger)
		{
			this._keyPressCounter++;
			this._encounterTimer.ResetTimer();
			GameDirector.instance.ClearFromBlack();
		}
		else if (_preLinger)
		{
			if (this._encounterTimer.ElapsedTime() >= intialLinger)
			{
				this._keyPressCounter = 0;
				this._preLinger = false;
				GameDirector.instance.FailedToLinger();
				this.LingerSame();
			}
		}
		
		if (GameDirector.instance.GetOvercomeBool())
		{
			GameDirector.instance.AbleToOvercome();
			if (this._keyPressCounter > 10)
			{
				GameDirector.instance.PlayerOvercame();
				Debug.Log("player wins");
			}
		}
		//*************************************************************************************************************/
	}

	private void LingerTimer()
	{
		this.lingerDuration -= Time.deltaTime;
		if (this.lingerDuration <= 0)
		{
			this._lingerLonger++;
			this.lingerDuration = this.baseDuration + this._lingerLonger;
			this._keyPressCounter = 0;
			this._preLinger = false;
			GameDirector.instance.NextOPStage();
			//Debug.Log("False from linger timer reset");
			GameDirector.instance.ResetLinger();
		}
	}

	private void LingerSame()
	{
		this.lingerDuration = this.baseDuration + this._lingerLonger;
	}
}