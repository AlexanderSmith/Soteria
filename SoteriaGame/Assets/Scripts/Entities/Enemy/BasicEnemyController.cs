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
	public float chaseSpeed = 10.0f;
	public float patrolSpeed = 5.0f;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	private float _stunDuration;
	public float stunTimer = 1.0f;
	
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
		//CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Unaware") as Texture;
		//this.renderer.material.mainTexture = CurrentTexture;
	}
	
	// Update is called once per frame
	void Update () 
	{
		_distance = Vector3.Distance(this.transform.position, player.transform.position);
		if (!this._stunned)
		{
			//_encounterManager.CheckPlayerDistance(this.gameObject, this._dead);
			GameDirector.instance.CheckPlayerDistance(this.gameObject, this._dead);
		}
		else
		{
			this._agent.Stop(false);
			this.Stunned();
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
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Alert") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
		this.transform.LookAt(player.transform.position);
	}
	
	public void ChasePlayer()
	{
		_agent.Resume();
		_agent.speed = chaseSpeed;
		_anim.SetBool ("Aggro", true);
		_anim.SetBool ("Alert", false);
		_anim.SetBool ("Moving", false);
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Attack") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
		_agent.SetDestination (player.transform.position);
		//Debug.Log("Enemy chasing");
	}
	
	public void OverwhelmPlayer()
	{
		//anim.SetBool ("Overpower", true);
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Attack") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
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
			_agent.speed = patrolSpeed;
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

//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Unaware") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
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
		//anim.SetBool ("Aggro", false);
		//anim.SetBool ("Overpower", false);
		_opCounter = 1;
	}

	public void DestroyMe()
	{
		GameDirector.instance.KillEnemy();
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
