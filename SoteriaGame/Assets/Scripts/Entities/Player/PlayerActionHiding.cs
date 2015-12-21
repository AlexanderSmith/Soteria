﻿using UnityEngine;

public class PlayerActionHiding : IPlayerAction
{
	private float _horizontal;
	private float _vertical;


	public void PlayerAction(Player inPlayer)
	{
		this._horizontal = Input.GetAxisRaw("Horizontal");
		this._vertical = Input.GetAxisRaw("Vertical");
//
//		if (!this._horizontal.Equals(0) || !this._vertical.Equals(0))
//		{
//			inPlayer.OutOfHide();
//		}

		if (Input.GetKeyUp(KeyCode.Space) && inPlayer.GetComponent<Animator>().GetBool("HideIdle"))
		{
			inPlayer.OutOfHide();
			//GameDirector.instance.ChangeGameState(GameStates.Normal);
		}
	}

	public void InitializeValues(Player inPlayer)
	{
	}
}