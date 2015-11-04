using UnityEngine;
using System.Collections;

public class PlayerActionMusicPuzzle : IPlayerAction
{
	private bool _lingering;
	private int _keyPressCounter;
	private GameObject controller;

	public void PlayerAction(Player inPlayer)
	{
		//this.InitializeValues(inPlayer);
		this.FightMusicBoss();
	}

	public void InitializeValues(Player inPlayer)
	{
		if (!inPlayer.encounterVariables)
		{
			this._keyPressCounter = 0;
			inPlayer.FlipEncounterBool();
			controller = GameObject.Find("MusicPuzzleControl");
		}
	}

	void FightMusicBoss()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!_lingering)
			{
				_lingering = true;
				GameDirector.instance.BeginLingering();
			}
			this._keyPressCounter++;
			GameDirector.instance.EncounterClear();
			controller.GetComponent<MusicPuzzleController>().GetBoss().GetComponentInChildren<MusicBossController>().FightingBoss();
		}
	}
}