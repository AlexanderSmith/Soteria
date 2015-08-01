﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour 
{
	public float moveSpeed = 5.0f;
	public float rotationSpeed = 20.0f;
	public float MoveAngleCorrection = 45.0f;
	public bool encounterVariables = false;

	private Animator _animator;
	private Vector3 _direction;

	private IPlayerAction _currentAction;
	private IPlayerAction normalAction = new PlayerActionNormal();
	private IPlayerAction encounterAction = new PlayerActionEncounter();

	private enum PlayerState
	{
		NORMAL,
		ENCOUNTER,
	};

	private PlayerState _currentState;
	
	void Start ()
	{
		this._animator = this.gameObject.GetComponent<Animator>();
		PlayerActionNormal();
		this._currentState = PlayerState.NORMAL;
	}

	void FixedUpdate () 
	{
		this._animator.SetBool("Moving", false);
		//this.ApplyDirection();
		this._currentAction.PlayerAction(this);
	}

//	public void Move()
//	{
//		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
//		this.ApplyMovement(moveDir);
//		this._animator.SetBool("Moving", true);
//	}
//	
//	public void ApplyMovement(Vector3 inDirection)
//	{
//		this.transform.rigidbody.MovePosition((inDirection * this.moveSpeed * Time.deltaTime) + this.transform.rigidbody.position);
//		this._direction = inDirection;
//	}
//
//	private void ApplyDirection()
//	{
//		if (this._direction != Vector3.zero)
//		{	
//			Quaternion targetRot = Quaternion.LookRotation (this._direction, Vector3.up);
//			Quaternion rot = Quaternion.Lerp (this.rigidbody.rotation, targetRot, rotationSpeed * Time.deltaTime);
//			this.rigidbody.MoveRotation (rot);
//		}
//	}

	private void PrivateFlipEncounterBool()
	{
		if (!this.encounterVariables)
		{
			this.encounterVariables = true;
		}
		else
		{
			this.encounterVariables = false;
		}
	}

	public void FlipEncounterBool()
	{
		this.PrivateFlipEncounterBool ();
	}

	private void SwitchPlayerAction(IPlayerAction newAction)
	{
		this._currentAction = newAction;
	}

	public void PlayerActionEncounter()
	{
		this.SwitchPlayerAction(encounterAction);
	}

	public void PlayerActionNormal()
	{
		this.SwitchPlayerAction(normalAction);
	}

	public void Moving()
	{
		this._animator.SetBool ("Moving", true);
	}

	public void BeginLingering()
	{
		this._animator.SetBool("PreOvercome", true);
	}

	public void ResetLinger()
	{
		this._animator.SetBool("PreOvercome", false);
	}

	public void HideDown()
	{
		this._animator.SetBool ("HideUp", false);
		this._animator.SetBool ("HideDown", true);
	}

	public void HideUp()
	{
		this._animator.SetBool ("HideDown", false);
		this._animator.SetBool ("HideIdle", false);
		this._animator.SetBool ("HideUp", true);
	}

	public void HideIdle()
	{
		this._animator.SetBool ("HideUp", false);
		this._animator.SetBool ("HideIdle", true);
	}

	public void Overcome()
	{
		this._animator.SetBool ("Overcome", true);
	}
	
	public void ResetEncounter()
	{
		this._animator.SetBool ("Overcome", false);
		this._animator.SetBool("PreOvercome", false);
		this.FlipEncounterBool();
	}
	
	public void AddFear()
	{
		this._animator.SetBool("Encounter", true);
	}
	
	public void RemoveFear()
	{
		this._animator.SetBool("Encounter", false);
	}
}