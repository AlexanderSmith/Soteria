using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour{

    public Transform player;
	Transform safeArea;
	GameObject agent;
	GameObject currentEnemy;
	bool playerInLight;
	EncounterManager encounterManager;

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
        //player.GetComponent<EncounterMovementController>().CheckEscape();
		encounterManager = manager;
    }

	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "Enemy")
		{
			//Debug.Log(enemy.tag);
			enemy.gameObject.GetComponent<BasicEnemyController>().PushBack();
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Debug.Log("player exiting");
			Debug.Log (encounterManager.name);
			encounterManager.StopEncounter();
		}
	}

	public void CurrentEnemy(GameObject enemy)
	{
		currentEnemy = enemy;
	}
}
