using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	public GameObject player;
	private EncounterManager encounterManager;
	private NavMeshAgent agent;
	private float distance = 0.0f;
	private float pushBack = 100.0f;
	//private Texture CurrentTexture;
	private Animator anim;
	private bool dead = false;
	private int opCounter = 1;

	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	private float _chaseSpeed = 10.0f;
	private float _patrolSpeed = 5.0f;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	
	// Use this for initialization
	public void Initialize(EncounterManager encMan)
	{
		encounterManager = encMan;
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		//CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Unaware") as Texture;
		//this.renderer.material.mainTexture = CurrentTexture;
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(this.transform.position, player.transform.position);	
		encounterManager.CheckPlayerDistance(this.gameObject, this.dead);
	}
	
	public void EndEncounter (bool status)
	{
		//staystill = status;
	}
	
	public void LookAtPlayer()
	{
		anim.SetBool ("Alert", true);
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Alert") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
		this.transform.LookAt(player.transform.position);
	}
	
	public void ChasePlayer()
	{
		agent.speed = _chaseSpeed;
		anim.SetBool ("Aggro", true);
		anim.SetBool ("Alert", false);
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Attack") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
		agent.SetDestination (player.transform.position);
		//Debug.Log("Enemy chasing");
	}
	
	public void OverwhelmPlayer()
	{
		//anim.SetBool ("Overpower", true);
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Attack") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
		agent.Stop();
	}
	
	public void Unaware()
	{
		anim.SetBool ("Aggro", false);
		anim.SetBool ("Alert", false);
		anim.SetBool ("Overpower", false);
		anim.SetBool ("Cower", false);
		opCounter = 1;
		if (patrolLocations.Length > 0)
		{
			anim.SetBool ("Moving", true);
			agent.speed = _patrolSpeed;
			if (agent.remainingDistance < agent.stoppingDistance)
			{
				anim.SetBool ("Moving", false);
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

			agent.destination = patrolLocations [_patrolIndex].position;
		}
		else
		{
			anim.SetBool ("Moving", false);
		}

//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Unaware") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
	}
	
	public float GetDistance()
	{
		return this.distance;
	}
	
	public void PushBack()
	{
		this.gameObject.rigidbody.AddForce(-this.gameObject.transform.forward * pushBack, ForceMode.Impulse);
	}

	public void Overpower()
	{
		switch (opCounter)
		{
		case 1:
			anim.SetBool ("Overpower", true);
			break;
		case 2:
			anim.SetBool ("OP 2", true);
			break;
		case 3:
			anim.SetBool ("OP 3", true);
			break;
		}
	}

	public void ResetOverpower()
	{
		anim.SetBool ("Overpower", false);
		anim.SetBool ("OP 2", false);
		anim.SetBool ("OP 3", false);
	}

	public void Cower()
	{
		anim.SetBool ("Cower", true);
		dead = true;
		anim.SetBool ("Aggro", false);
		anim.SetBool ("Overpower", false);
		opCounter = 1;
	}

	public void DestroyMe()
	{
		encounterManager.DestroyMe ();
	}

	public void NextOPStage()
	{
		opCounter++;
	}
}
