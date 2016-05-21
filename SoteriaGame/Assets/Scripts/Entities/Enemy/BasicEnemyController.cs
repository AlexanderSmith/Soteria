using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	public GameObject player;
	public NavMeshAgent _agent;
	public float distance = 0.0f;
	private Animator _anim;
	public bool dead = false;
	private bool _stunned = false;
	private int _opCounter = 1;

	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	public float chaseSpeed = 10.0f;
	public float patrolSpeed = 5.0f;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	private float _stunDuration;
	public float stunTimer = 5.0f;
	
	public float lookAtDistance = 45.0f;
	public float attackRange = 35.0f;
	public float overwhelmRange = 15.0f;

	public bool theaterEnemy;

	private IEnemyAction _currentAction;
	private IEnemyAction _notVisibleEA = new EnemyActionNotVisible();
	private IEnemyAction _visibleEA = new EnemyActionVisible();
	private IEnemyAction _hiddenEA = new EnemyActionHidden();
	private IEnemyAction _hiddenTileEA = new EnemyActionHiddenTile();
	private IEnemyAction _theaterEA = new EnemyActionTheater();
	private IEnemyAction _stunnedEA = new EnemyActionStunned();
	private IEnemyAction _suitEA = new EnemyActionSuit();
	
	public bool playerVisible;
	public float fieldOfVision = 125.0f;
	public SphereCollider sphereCollider;
	public float eyeHeightOffset;
	
	// Use this for initialization
	void Start ()
	{
		if (player == null)
		{
			player = GameDirector.instance.GetPlayer().gameObject;
		}
		_agent = GetComponent<NavMeshAgent> ();
		_anim = GetComponent<Animator> ();
		this._stunDuration = this.stunTimer;
//		normEC = this.GetComponentInChildren<EnemyControllerNormal>();
//		hiddenEC = this.GetComponentInChildren<EnemyControllerNormal>();
//		hiddenTileEC = this.GetComponentInChildren<EnemyControllerNormal>();
		if (GameDirector.instance.GetGameState() == GameStates.Suit)
		{
			this.SuitAction();
		}
		else if (!this.theaterEnemy)
		{
			this.NotVisibleAction();
		}
		else
		{
			this.TheaterAction();
		}
		this.sphereCollider = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this._currentAction.EnemyAction(this);
//		if (this._stunned)
//		{
//			this._agent.Stop();
//			this.Stunned();
//		}
//		else if (GetComponent<EnemySight>().IsPlayerVisible() && GameDirector.instance.GetPlayer().GetPlayerState() != PlayerState.Dialogue)
//		{
//			if (GameDirector.instance.GetGameState() == GameStates.HiddenTile)
//			{
//				this.LookAtPlayer();
//				GameDirector.instance.PlayerOnObservatoryTile();
//			}
//			else if (GameDirector.instance.GetGameState() == GameStates.Hidden)
//			{
//				this.Unaware();
//			}
//			else if (!this._dead)
//			{
//				this._distance = Vector3.Distance(this.transform.position, player.transform.position);
//				if (this._distance <= this.overwhelmRange)
//				{
//					if (GameDirector.instance.GetGameState() != GameStates.Suit)
//					{
//						this.OverwhelmPlayer();
//						GameDirector.instance.Encounter(this.gameObject);
//					}
//					else
//					{
//						this.LookAtPlayer();
//					}
//				}
//				else if (this._distance <= this.attackRange)
//				{
//					this.ChasePlayer();
//				}
//				else
//				{
//					this.LookAtPlayer();
//				}
//			}
//		}
//		else if (this._dead)
//		{
//			this._agent.Stop();
//
//		}
//		else
//		{
//			if (GameDirector.instance.GetPlayer().GetPlayerState() != PlayerState.Dialogue)
//			{
//				this.Unaware();
//			}
//		}
	}
	
	public void LookAtPlayer()
	{
		_anim.SetBool ("Alert", true);
		this._agent.Stop();
		this.transform.LookAt(player.transform.position);
	}
	
	public void ChasePlayer()
	{
		_agent.Resume();
		_agent.speed = chaseSpeed;
		_anim.SetBool ("Aggro", true);
		_anim.SetBool ("Alert", false);
		_anim.SetBool ("Moving", false);
		_agent.SetDestination (player.transform.position);
		//Debug.Log("Enemy chasing");
	}
	
	public void OverwhelmPlayer()
	{
		_agent.Stop();
		this.ForceOverwhelmState();
	}

	void ForceOverwhelmState()
	{
		this.transform.LookAt(player.transform.position);
		_anim.SetBool ("Alert", true);
		_anim.SetBool ("Aggro", true);
		_anim.SetBool ("Alert", false);
		_anim.SetBool ("Moving", false);
	}
	
	public void Unaware()
	{
		_agent.Resume();
		_anim.SetBool ("Aggro", false);
		_anim.SetBool ("Alert", false);
		_anim.SetBool ("Overpower", false);
		_anim.SetBool ("Cower", false);
		_opCounter = 1;
		if (patrolLocations.Length > 0)
		{
			_anim.SetBool ("Moving", true);
			_agent.speed = patrolSpeed;
			if (_agent.remainingDistance < _agent.stoppingDistance)
			{
				_anim.SetBool ("Moving", false);
				_patrolTimer += Time.deltaTime;
				// Bad -- Needs to be re-visited later
				this._agent.gameObject.transform.rotation = this.patrolLocations[this._patrolIndex].rotation;
				if (_patrolTimer >= waitTime)
				{
					if (_patrolIndex == patrolLocations.Length - 1)
					{
						_patrolIndex = 0;
					}
					else
					{
						_patrolIndex++;
					}
					_patrolTimer = 0.0f;
				}
			}

			_agent.destination = patrolLocations [_patrolIndex].position;
		}
		else
		{
			_anim.SetBool ("Moving", false);
		}
	}
	
	public float GetDistance()
	{
		return this.distance;
	}

	public void Stun()
	{
		this._stunned = true;
		this.StunnedAction();
	}

	public bool GetStunStatus()
	{
		return this._stunned;
	}

	public void Overpower()
	{
		switch (_opCounter)
		{
		case 1:
			_anim.SetBool ("Overpower", true);
			break;
		case 2:
			_anim.SetBool ("OP 2", true);
			break;
		case 3:
			_anim.SetBool ("OP 3", true);
			break;
		}
	}

	public void ResetOverpower()
	{
		_anim.SetBool ("Overpower", false);
		_anim.SetBool ("OP 2", false);
		_anim.SetBool ("OP 3", false);
	}

	public void Cower()
	{
		_anim.SetBool ("Cower", true);
		dead = true;
		_opCounter = 1;
	}

	// Called from animation controller at end of Cower animation
	public void DestroyMe()
	{
		//GameDirector.instance.KillEnemy();
		this.gameObject.SetActive(false);
	}

	public void NextOPStage()
	{
		_opCounter++;
	}

	public void Stunned()
	{
		this._agent.Stop();
		this._stunDuration -= Time.deltaTime;
		if (this._stunDuration <= 0)
		{
			this._stunned = false;
			this._stunDuration = stunTimer;
			if (this.theaterEnemy)
			{
				this.TheaterAction();
			}
			else
			{
				this.NotVisibleAction();
			}
		}
	}

	public NavMeshAgent GetAgent()
	{
		return this._agent;
	}

	void SwitchAction(IEnemyAction inAction)
	{
		this._currentAction = inAction;
	}

	public IEnemyAction GetCurrentAction()
	{
		return this._currentAction;
	}

	public void NotVisibleAction()
	{
		_agent.Resume();
		this.SwitchAction(this._notVisibleEA);
	}

	public void VisibleAction()
	{
		this.SwitchAction(this._visibleEA);
	}

	public void HiddenAction()
	{
		this.SwitchAction(this._hiddenEA);
	}

	public void HiddenTileAction()
	{
		this.SwitchAction(this._hiddenTileEA);
	}

	public void StunnedAction()
	{
		this.SwitchAction(this._stunnedEA);
	}

	public void TheaterAction()
	{
		_agent.Resume();
		this.SwitchAction (this._theaterEA);
	}

	public void SuitAction()
	{
		this.SwitchAction(this._suitEA);
	}
}