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
		if (!this._stunned && GameDirector.instance.GetGameState() != GameStates.Hidden)
		{
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
			_agent.Resume();
		}
	}

	public void Stun()
	{
		this._stunned = true;
	}
}
