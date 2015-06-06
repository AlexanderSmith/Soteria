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
	public bool dead = false;
	
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
		encounterManager.CheckPlayerDistance(this.gameObject);
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
		anim.SetBool ("Aggro", true);
		anim.SetBool ("Alert", false);
//		CurrentTexture = Resources.Load("Textures/_OldTextures/ShadowCreature Attack") as Texture;
//		this.renderer.material.mainTexture = CurrentTexture;
		agent.SetDestination (player.transform.position);
		Debug.Log("Enemy chasing");
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
		anim.SetBool ("Moving", false);
		anim.SetBool ("Cower", false);
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
		anim.SetBool ("Overpower", true);
	}

	public void ResetOverpower()
	{
		anim.SetBool ("Overpower", false);
	}

	public void Cower()
	{
		anim.SetBool ("Cower", true);
		dead = true;
		anim.SetBool ("Aggro", false);
		anim.SetBool ("Overpower", false);
	}

	public void DestroyMe()
	{
		encounterManager.DestroyMe ();
	}
}
