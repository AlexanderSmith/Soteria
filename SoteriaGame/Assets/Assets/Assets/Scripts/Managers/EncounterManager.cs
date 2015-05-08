using UnityEngine;
using System.Collections;

public class EncounterManager : MonoBehaviour {

	float lookAtDistance;
	float attackRange;
	float overwhelmRange;

	GameObject[] enemies;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void Initialize()
	{
		lookAtDistance = 25.0f;
		attackRange = 15.0f;
		overwhelmRange = 5.0f;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		LinkToEnemy();
	}

	void LinkToEnemy()
	{
		foreach (GameObject enemy in enemies) 
		{
			Debug.Log(enemy.name);
			enemy.GetComponent<BasicEnemyController>().Initialize(this);
		}
	}

    void StopEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StopEncounterMode();
    }

    void StartEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StartEncounterMode();
    }

	public void CheckPlayerDistance(GameObject enemy)
	{
		if (enemy.GetComponent<BasicEnemyController>().distance <= overwhelmRange)
		{
			enemy.GetComponent<BasicEnemyController>().SendMessage("OverwhelmPlayer");
		}
		else if (enemy.GetComponent<BasicEnemyController>().distance <= attackRange)
		{
			enemy.GetComponent<BasicEnemyController>().SendMessage ("ChasePlayer");
		}
		else if (enemy.GetComponent<BasicEnemyController>().distance <= lookAtDistance)
		{
			enemy.GetComponent<BasicEnemyController>().SendMessage("LookAtPlayer");
		}
	}

//    IEnumerator KickOffEncounter()
//    {
//        StartEncounter();
//        return null;
//    }
}
