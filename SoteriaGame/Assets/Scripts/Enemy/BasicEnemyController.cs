using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	public GameObject player;
	private EncounterManager _encounterManager;
	private NavMeshAgent _agent;
	private float _distance = 0.0f;
	private float _pushBack = 100.0f;
	//private Texture CurrentTexture;
	private Animator _anim;
	private bool _dead = false;
	private bool _stunned = false;
	private int _opCounter = 1;

	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	private float _chaseSpeed = 10.0f;
	private float _patrolSpeed = 5.0f;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	private float _stunDuration;
	public float stunTimer = 1.0f;

	private bool _playerVisible;
	public float fieldOfVision = 90.0f;
	private SphereCollider _sphereCollider;

	public float lookAtDistance = 45.0f;
	public float attackRange = 35.0f;
	public float overwhelmRange = 15.0f;
	
	// Use this for initialization
	public void Initialize(EncounterManager encMan)
	{
		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
		}
		_encounterManager = encMan;
		_agent = GetComponent<NavMeshAgent> ();
		_anim = GetComponent<Animator> ();
		this._stunDuration = this.stunTimer;
		this._sphereCollider = this.gameObject.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		_distance = Vector3.Distance(this.transform.position, player.transform.position);
		/*if (!this._stunned)
		{
			GameDirector.instance.GetEncounterManager().CheckPlayerDistance(this.gameObject, this._dead);
		}
		else*/
		if (this._stunned)
		{
			this._agent.Stop(false);
			this.Stunned();
		}
		else if (this._playerVisible)
		{
//			this.LookAtPlayer();
//			GameDirector.instance.GetEncounterManager().CheckPlayerDistance(this.gameObject, this._dead);
			if (this._distance <= this.overwhelmRange)
			{
				GameDirector.instance.GetEncounterManager().Encounter(this.gameObject);
				this.OverwhelmPlayer();
			}
			else if (this._distance <= this.attackRange)
			{
				this.ChasePlayer();
			}
			else if (this._distance <= this.lookAtDistance)
			{
				this.LookAtPlayer();
			}
			else
			{
				this.Unaware();
			}
		}
		else
		{
			this.Unaware();
		}
	}

//	float distance = enemy.GetComponent<BasicEnemyController>().GetDistance();
//	if (!inDead && GameDirector.instance.GetGameState() != GameStates.Hidden && GameDirector.instance.GetGameState() != GameStates.HiddenTile)
//	{
//		if (distance <= overwhelmRange)
//		{
//			this.Encounter(enemy);
//			enemy.GetComponent<BasicEnemyController>().OverwhelmPlayer();
//		}
//		else if (distance <= attackRange)
//		{
//			enemy.GetComponent<BasicEnemyController>().ChasePlayer();
//		}
//		else if (distance <= lookAtDistance)
//		{
//			enemy.GetComponent<BasicEnemyController>().LookAtPlayer();
//		}
//		//			else
//		//			{
//		//				enemy.GetComponent<BasicEnemyController>().Unaware();
//		//			}
//	}
//	else if (GameDirector.instance.GetGameState() == GameStates.HiddenTile)
//	{
//		if (distance <= lookAtDistance)
//		{
//			enemy.GetComponent<BasicEnemyController>().LookAtPlayer();
//		}
//		this.TileTimer();
//	}
//	else if (GameDirector.instance.GetGameState() == GameStates.Hidden)
//	{
//		enemy.GetComponent<BasicEnemyController>().Unaware();
//	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._playerVisible = false;

			Vector3 direction = player.transform.position - this.gameObject.transform.position;
			float angle = Vector3.Angle(direction, this.gameObject.transform.forward);

			if(angle < fieldOfVision * 0.5f)
			{
				RaycastHit hit;

				if(Physics.Raycast(this.gameObject.transform.position + this.gameObject.transform.up, direction, out hit, this._sphereCollider.radius))
				{
					if(hit.collider.gameObject.Equals(player.gameObject))
					{
						this._playerVisible = true;
						if (this._distance <= this.overwhelmRange)
						{
							GameDirector.instance.GetEncounterManager().Encounter(this.gameObject);
							this.OverwhelmPlayer();
						}
						else if (this._distance <= this.attackRange)
						{
							this.ChasePlayer();
						}
						else if (this._distance <= this.lookAtDistance)
						{
							this.LookAtPlayer();
						}
						else
						{
							this.Unaware();
						}
					}
				}
				//Debug.DrawRay(this.gameObject.transform.position + this.gameObject.transform.up, direction, Color.white, 200, false);
			}
		}
	}
	
	public void EndEncounter (bool status)
	{
		//staystill = status;
	}
	
	public void LookAtPlayer()
	{
		_anim.SetBool ("Alert", true);
		this._agent.Stop(false);
		this.transform.LookAt(player.transform.position);
	}
	
	public void ChasePlayer()
	{
		_agent.Resume();
		_agent.speed = _chaseSpeed;
		_anim.SetBool ("Aggro", true);
		_anim.SetBool ("Alert", false);
		_anim.SetBool ("Moving", false);
		_agent.SetDestination (player.transform.position);
		//Debug.Log("Enemy chasing");
	}
	
	public void OverwhelmPlayer()
	{
		_agent.Stop(false);
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
			_agent.speed = _patrolSpeed;
			if (_agent.remainingDistance < _agent.stoppingDistance)
			{
				_anim.SetBool ("Moving", false);
				_patrolTimer += Time.deltaTime;
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
		return this._distance;
	}
	
	public void PushBack()
	{
		this.gameObject.GetComponent<Rigidbody>().AddForce(-this.gameObject.transform.forward * _pushBack, ForceMode.Impulse);
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
		_dead = true;
		_opCounter = 1;
	}

	public void DestroyMe()
	{
		GameDirector.instance.GetEncounterManager().DestroyMe();
	}

	public void NextOPStage()
	{
		_opCounter++;
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

	public NavMeshAgent GetAgent()
	{
		return this._agent;
	}
}
