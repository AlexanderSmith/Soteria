using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour{

	private EncounterManager encounterManager;
	private GameObject agent;
	//public Transform player;
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
        this.GetComponent<Light>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
    }

    public void DisableSafetyLight()
    {
        this.GetComponent<Light>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
    }

    public void Initialize(EncounterManager manager)
    {
		agent = GameObject.FindWithTag ("SafetyLight Agent");
		agent.GetComponent<SafetyLightAgentMovement> ().EnableAgent ();
		EnableSafetyLight();
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
