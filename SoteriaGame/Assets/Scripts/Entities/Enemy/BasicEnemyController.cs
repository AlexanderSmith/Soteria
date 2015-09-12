using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {
	
	public GameObject player;
	private NavMeshAgent _agent;
	private float _distance = 0.0f;
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
	public void Initialize()
	{
		if (this.player == null)
		{
			this.player = GameObject.FindWithTag("Player");
		}
		this._agent = GetComponent<NavMeshAgent> ();
		this._anim = GetComponent<Animator> ();
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
			this._agent.Stop();
			this.Stunned();
		}
		else if (this._playerVisible)
		{
			//			this.LookAtPlayer();
			//			GameDirector.instance.GetEncounterManager().CheckPlayerDistance(this.gameObject, this._dead);
			if (this._distance <= this.overwhelmRange)
			{
				GameDirector.instance.Encounter(this.gameObject);
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
							GameDirector.instance.Encounter(this.gameObject);
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
	
	public void LookAtPlayer()
	{
		this._anim.SetBool ("Alert", true);
		this._agent.Stop();
		this.transform.LookAt(player.transform.position);
	}
	
	public void ChasePlayer()
	{
		this._agent.Resume();
		this._agent.speed = _chaseSpeed;
		this._anim.SetBool ("Aggro", true);
		this._anim.SetBool ("Alert", false);
		this._anim.SetBool ("Moving", false);
		this._agent.SetDestination (player.transform.position);
		//Debug.Log("Enemy chasing");
	}
	
	public void OverwhelmPlayer()
	{
		this._agent.Stop();
	}
	
	public void Unaware()
	{
		this._agent.Resume();
		this._anim.SetBool ("Aggro", false);
		this._anim.SetBool ("Alert", false);
		this._anim.SetBool ("Overpower", false);
		this._anim.SetBool ("Cower", false);
		this._opCounter = 1;
		if (this.patrolLocations.Length > 0)
		{
			this._anim.SetBool ("Moving", true);
			this._agent.speed = this._patrolSpeed;
			if (this._agent.remainingDistance < this._agent.stoppingDistance)
			{
				this._anim.SetBool ("Moving", false);
				this._patrolTimer += Time.deltaTime;
				if (this._patrolTimer >= this.waitTime)
				{
					if (this._patrolIndex == this.patrolLocations.Length - 1)
					{
						this._patrolIndex = 0;
					}
					else
					{
						this._patrolIndex++;
					}
					this._patrolTimer = 0.0f;
				}
			}
			
			this._agent.destination = patrolLocations [_patrolIndex].position;
		}
		else
		{
			this._anim.SetBool ("Moving", false);
		}
	}
	
	public float GetDistance()
	{
		return this._distance;
	}
	
	public void Stun()
	{
		this._stunned = true;
	}
	
	public void Overpower()
	{
		switch (this._opCounter)
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
	
	public void Cower()
	{
		this._anim.SetBool ("Cower", true);
		this._dead = true;
		this._opCounter = 1;
	}
	
	public void DestroyMe()
	{
		GameDirector.instance.KillEnemy();
	}
	
	public void NextOPStage()
	{
		this._opCounter++;
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
	
	public NavMeshAgent GetAgent()
	{
		return this._agent;
	}
}
