using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour 
{
	public float moveSpeed = 5.0f;
	public float rotationSpeed = 20.0f;
	public float MoveAngleCorrection = 45.0f;

	private Animator _animator;
	private Vector3 _direction;

	private IPlayerAction _currentAction;

	private enum PlayerState
	{
		normal,
		encounter,
	};

	private PlayerState _currentState;
	
	void Start ()
	{
		this._animator = this.gameObject.GetComponent<Animator>();
		this.SwitchPlayerAction(new PlayerActionNormal());
		this._currentState = PlayerState.normal;
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

	public void Encounter()
	{

	}

	private void SwitchPlayerAction(IPlayerAction newAction)
	{
		this._currentAction = newAction;
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
}