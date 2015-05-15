using UnityEngine;
using System.Collections;


/// <summary>
/// Encounter manager.
/// 
/// Possible Additions: 
/// Set the Encounter States -> what's the state of the player: winning, losing, escaping ecc.
/// 
/// </summary>


public class EncounterManager : MonoBehaviour {

	float lookAtDistance;
	float attackRange;
	float overwhelmRange;
	GameObject[] enemies;
	GameObject safetyLight;
	bool lightOn = false;
	/********************************************************************************
	//Make enum for enemy state to use for switch statements in CheckPlayerDistance()
	********************************************************************************/

	enum EncounterState
	{
		Active,
		ActiveLight,
		Inactive,
	};

	EncounterState currentState;
//    IEnumerator KickOffEncounter()
//    {
//        StartEncounter();
//        return null;
//    }

    // Use this for initialization
    void Start()
    {
//        StartCoroutine(KickOffEncounter());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
		lookAtDistance = 25.0f;
		attackRange = 15.0f;
		overwhelmRange = 5.0f;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		LinkToEnemy();
		safetyLight = GameObject.FindGameObjectWithTag("SafetyLight Agent");
		currentState = EncounterState.Inactive;
		//Debug.Log (safetyLight.name);
    }

	void LinkToEnemy()
	{
		foreach (GameObject enemy in enemies) 
		{
			enemy.GetComponent<BasicEnemyController>().Initialize(this);
		}
	}

	public void CheckPlayerDistance(GameObject enemy)
	{
		if (enemy.GetComponent<BasicEnemyController>().GetDistance() <= overwhelmRange)
		{
			Encounter(enemy);
			enemy.GetComponent<BasicEnemyController>().OverwhelmPlayer(lightOn);
		}
		else if (enemy.GetComponent<BasicEnemyController>().GetDistance() <= attackRange)
		{
			enemy.GetComponent<BasicEnemyController>().ChasePlayer();
		}
		else if (enemy.GetComponent<BasicEnemyController>().GetDistance() <= lookAtDistance)
		{
			enemy.GetComponent<BasicEnemyController>().LookAtPlayer();
		}
		else
		{
			enemy.GetComponent<BasicEnemyController>().Unaware();
		}
	}

	public void Encounter(GameObject enemy)
	{
		if (currentState == EncounterState.Inactive)
		{
			currentState = EncounterState.Active;
			StartEncounter();
		}
		safetyLight.GetComponentInChildren<SafetyLightController> ().CurrentEnemy(enemy);
	}

    public void StopEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StopEncounterMode();
		lightOn = false;
    }

    public void StartEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StartEncounterMode();
    }

	public void InitializeSafetyLight()
	{
		safetyLight.GetComponentInChildren<SafetyLightController>().Initialize(this);
		lightOn = true;
	}

	public void KillSafetyLight()
	{
		safetyLight.GetComponentInChildren<SafetyLightController>().DisableSafetyLight();
		Debug.Log ("Killing light");
	}

//	public float GetOverwhelmRange()
//	{
//		return this.overwhelmRange;
//	}
//
//	public float GetAttackRange()
//	{
//		return this.attackRange;
//	}
}
