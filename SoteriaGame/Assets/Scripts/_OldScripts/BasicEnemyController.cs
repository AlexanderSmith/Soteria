using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	public GameObject player;
	private EncounterManager encounterManager;
	private NavMeshAgent agent;
	private float distance = 0.0f;

//    BasicAggroSystem aggroManager;
//
//	private bool staystill; 
	Texture CurrentTexture;

	// Use this for initialization
	public void Initialize(EncounterManager encMan)
	{
		//player = GameObject.FindWithTag("Player");
		encounterManager = encMan;
		agent = GetComponent<NavMeshAgent> ();
        //aggroManager = new BasicAggroSystem();
		//staystill = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (!staystill)
//		{
//        	aggroManager.AggroCheckAndBasicMove(lookAtDistance, attackRange, overwhelmRange, target.transform, this.transform);
//		}
		distance = Vector3.Distance(this.transform.position, player.transform.position);
		encounterManager.CheckPlayerDistance(this.gameObject);
	}

	public void EndEncounter (bool status)
	{
		//staystill = status;
	}

	public void LookAtPlayer()
	{
		CurrentTexture = Resources.Load("ShadowCreatureAlert") as Texture;
		this.renderer.material.mainTexture = CurrentTexture;
		this.transform.LookAt(player.transform.position);
	}

	public void ChasePlayer()
	{
		CurrentTexture = Resources.Load("ShadowCreature_Attack") as Texture;
		this.renderer.material.mainTexture = CurrentTexture;

			agent.SetDestination (player.transform.position);
			Debug.Log("Enemy chasing");
	}

	public void OverwhelmPlayer(bool lightOn)
	{
		CurrentTexture = Resources.Load("ShadowCreature_Attack") as Texture;
		this.renderer.material.mainTexture = CurrentTexture;
		agent.Stop();
		player.GetComponent<EncounterMovementController>().Overwhelm(this.transform, lightOn);
	}

	public void Unaware()
	{
		CurrentTexture = Resources.Load("ShadowCreature Unaware") as Texture;
		this.renderer.material.mainTexture = CurrentTexture;
	}

	public float GetDistance()
	{
		return this.distance;
	}
}
