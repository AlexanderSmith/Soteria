using UnityEngine;
using System.Collections;

public class EyeballShadowCreatureController : MonoBehaviour
{
	private GameObject _player;
	private NavMeshAgent _agent;
	private Animator _anim;
	private bool _dead = false;
	private bool _stunned = false;
	private float _chaseSpeed = 10.0f;
	private float _stunDuration;
	public float stunTimer = 1.0f;
	private float _overwhelmRange = 15.0f;
	private int _opCounter = 1;
	public float lookAtDistance = 45.0f;

	void Start()
	{
		_player = GameObject.FindWithTag("Player");
		_agent = GetComponent<NavMeshAgent> ();
		_anim = GetComponent<Animator> ();
		this._stunDuration = this.stunTimer;
		_agent.speed = _chaseSpeed;
		_anim.SetBool ("Aggro", true);
		_anim.SetBool ("Alert", false);
		_anim.SetBool ("Moving", false);
	}

	void Update()
	{
		if (!this._stunned && GameDirector.instance.GetGameState() != GameStates.Hidden && GameDirector.instance.GetPlayer().GetPlayerState() != PlayerState.Dialogue)
		{
			this._agent.Resume();
			float distance = Vector3.Distance(this.transform.position, _player.transform.position);
			if (!_dead)
			{
				if (distance <= _overwhelmRange)
				{
					GameDirector.instance.Encounter(this.gameObject);
					OverwhelmPlayer();
				}
				else
				{
					this._agent.SetDestination(_player.transform.position);
				}
			}
		}
		else if (GameDirector.instance.GetGameState() == GameStates.Hidden)
		{
			Destroy(this.gameObject);
		}
		else
		{
			this._agent.Stop();
			this.Stunned();
		}
	}

	private void OverwhelmPlayer()
	{
		_agent.Stop();
	}

	private void Stunned()
	{
		this._stunDuration -= Time.deltaTime;
		if (this._stunDuration <= 0)
		{
			this._stunned = false;
			this._stunDuration = stunTimer;
			this._agent.Resume();
		}
	}

	public void Stun()
	{
		this._stunned = true;
	}

	public void Overpower()
	{
		switch (_opCounter)
		{
		case 1:
			this._anim.SetBool ("Overpower", true);
			break;
		case 2:
			this._anim.SetBool ("OP 2", true);
			break;
		case 3:
			this._anim.SetBool ("OP 3", true);
			break;
		}
	}
	
	public void ResetOverpower()
	{
		this._anim.SetBool ("Overpower", false);
		this._anim.SetBool ("OP 2", false);
		this._anim.SetBool ("OP 3", false);
	}

	public void NextOPStage()
	{
		_opCounter++;
	}

	public void Cower()
	{
		_anim.SetBool ("Cower", true);
		_dead = true;
		_opCounter = 1;
	}
}