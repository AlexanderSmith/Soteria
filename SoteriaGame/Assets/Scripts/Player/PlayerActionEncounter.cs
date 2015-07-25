using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActionEncounter : IPlayerAction
{
	private Timer _encounterTimer;
	private TimersType _timerType = TimersType.Encounter;
	private bool _isqtemode;
	private bool _preLinger = false;

	private int _keyPressCounter = 0;
	public float intialLinger = 1.0f;
	public float baseDuration = 2.0f;
	public float lingerDuration = 2.0f;
	private int _lingerLonger = 0;

	public void PlayerAction(Player inPlayer)
	{
		this.ProcessInput();
		this.InitializeTimer();
		this._encounterTimer.StartTimer();
		if (_preLinger)
		{
			this.LingerTimer();
		}
	}

	private void InitializeTimer()
	{
		this._encounterTimer = TimerManager.instance.Attach(this._timerType);
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
				//Debug.Log("player wins");
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