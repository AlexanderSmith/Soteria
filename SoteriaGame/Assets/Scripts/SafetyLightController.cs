using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour{

	private EncounterManager encounterManager;
	private GameObject agent;
	public Transform player;
	private Transform safeArea;
	private bool playerInLight;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
	}


    public void EnableSafetyLight()
    {
        this.light.enabled = true;
        this.collider.enabled = true;
    }

    public void DisableSafetyLight()
    {
        this.light.enabled = false;
        this.collider.enabled = false;
    }

    public void Initialize(EncounterManager manager)
    {
		agent = GameObject.FindWithTag ("SafetyLight Agent");
		agent.GetComponent<SafetyLightAgentMovement> ().EnableAgent ();
		EnableSafetyLight();
		GameDirector.instance.GetPlayer().GetComponentInChildren<PCController>().EnableStandardMovement();
		encounterManager = manager;
    }

	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "Enemy")
		{
			enemy.gameObject.GetComponent<BasicEnemyController>().PushBack();
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			encounterManager.StopEncounter();
		}
	}
}
