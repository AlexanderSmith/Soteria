using UnityEngine;
using System.Collections;

public enum PlayerState
{
	Normal,
	Dialogue
}

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour 
{
	public float moveSpeed = 5.0f;
	public float rotationSpeed = 20.0f;
	public float MoveAngleCorrection = 45.0f;
	public float hideDelay = 1f;
	public bool encounterVariables = false;

	private Animator _animator;
	private Vector3 _direction;

	private IPlayerAction _currentAction;
	private IPlayerAction _normalAction = new PlayerActionNormal();
	private IPlayerAction _encounterAction = new PlayerActionEncounter();
	private IPlayerAction _hidingAction = new PlayerActionHiding();
	private IPlayerAction _pauseAction = new PlayerActionPause();
	private IPlayerAction _noFighting = new PlayerActionNoFight();
	private IPlayerAction _cardPickup = new global::PlayerActionCardPickup();

	public PlayerState  playerState;

	void Start ()
	{
		PlayerActionNormal();
		playerState = PlayerState.Normal;
		this._animator = this.gameObject.GetComponent<Animator>();
	}

	void FixedUpdate () 
	{
		this._animator.SetBool("Moving", false);
		this._currentAction.PlayerAction(this);
	}

	public PlayerState GetPlayerState()
	{
		return this.playerState;
	}

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
		this.SwitchPlayerAction(_encounterAction);
	}

	public void PlayerActionNormal()
	{
		this.SwitchPlayerAction(_normalAction);
		playerState = PlayerState.Normal;
	}

	public void PlayerActionHiding()
	{
		StartCoroutine("IntoHide");
		this.SwitchPlayerAction(_hidingAction);
	}

	public void PlayerActionPause()
	{
		this.SwitchPlayerAction(_pauseAction);
		playerState = PlayerState.Dialogue;
	}

	public void PlayerActionCardPickup()
	{
		this.SwitchPlayerAction(_cardPickup);
	}

	public void PlayerActionNoFighting()
	{
		this.SwitchPlayerAction(_noFighting);
	}

	IEnumerator IntoHide()
	{
		this.HideDown();
		yield return new WaitForSeconds(hideDelay);
		this.HideIdle();
	}

	public void OutOfHide()
	{
		StartCoroutine("ExitHide");
	}

	IEnumerator ExitHide()
	{
		this.HideUp();
		yield return new WaitForSeconds(hideDelay);
		this.ResetHide();
		this.PlayerActionNormal();
	}

	public void Moving()
	{
		this._animator.SetBool("Moving", true);
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
		//this._animator.SetBool ("HideUp", false);
		this._animator.SetBool("HideDown", true);
	}

	public void HideUp()
	{
		this._animator.SetBool("HideUp", true);
		this._animator.SetBool("HideDown", false);
		this._animator.SetBool("HideIdle", false);
		//this.ResetHide();
	}

	public void ResetHide()
	{
		this._animator.SetBool("HideUp", false);
		GameDirector.instance.ChangeGameState(GameStates.Normal);
	}

	public void HideIdle()
	{
		//this._animator.SetBool ("HideUp", false);
		this._animator.SetBool("HideIdle", true);
	}

	public void Overcome()
	{
		this._animator.SetBool ("Overcome", true);
	}
	
	public void ResetEncounter()
	{
		this._animator.SetBool("Overcome", false);
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

	public void Footstep()
	{
		GameDirector.instance.PlayAudioClip(AudioID.WoodFootsteps);
	}
}