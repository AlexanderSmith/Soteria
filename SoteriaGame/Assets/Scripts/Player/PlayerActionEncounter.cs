using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActionEncounter : IPlayerAction
{
	private Timer _encounterTimer;
	private bool _isqtemode;
	private bool _preLinger = false;

	private int keyPressCounter = 0;
	private float intialLinger = 1.0f;
	private float lingerDuration = 2.0f;
	private int lingerLonger = 0;

	public void PlayerAction(Player player)
	{

	}
}