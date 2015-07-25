using UnityEngine;
using System.Collections;


/// <summary>
/// Encounter manager.
/// 
/// Possible Additions: 
/// Set the Encounter States -> what's the state of the player: winning, losing, escaping ecc.
/// 
/// </summary>

public enum EncounterState
{
	ACTIVE,
	INACTIVE,
	LINGERING
};

public class EncounterManager : MonoBehaviour
{
	private GameObject[] enemies;
	private GameObject safetyLight;
	private GameObject currentEnemy;

	private float lookAtDistance;
	private float attackRange;
	private float overwhelmRange;
	private float lightTimer = 20.0f;
	private float gameOverTimer = 30.0f;

	private int overcomeCounter = 0;

	private bool lightOn = false;
	private bool cooldown = false;
	private bool ableToOvercome = false;

	EncounterState currentState;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (cooldown)
		{
			LightCooldown();
		}
		if (currentState == EncounterState.ACTIVE)
		{
			GameOverTimer();
		}
    }

    public void Initialize()
    {
		lookAtDistance = 45.0f;
		attackRange = 35.0f;
		overwhelmRange = 15.0f;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		LinkToEnemy();
		safetyLight = GameObject.FindGameObjectWithTag("SafetyLight Agent");
		currentState = EncounterState.INACTIVE;
    }

	void LinkToEnemy()
	{
		foreach (GameObject enemy in enemies) 
		{
			Debug.Log(enemy.name);
			enemy.GetComponent<BasicEnemyController>().Initialize(this);
		}
	}

	public void CheckPlayerDistance(GameObject enemy)
	{
		if (enemy.GetComponent<BasicEnemyController>().dead == false && GameDirector.instance.GetGameState() != GameStates.Hidden)
		{
			if (enemy.GetComponent<BasicEnemyController>().GetDistance() <= overwhelmRange)
			{
				Encounter(enemy);
				//Debug.Log ("Distance" + enemy.GetComponent<BasicEnemyController>().GetDistance());
				enemy.GetComponent<BasicEnemyController>().OverwhelmPlayer();
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
	}

	public void Encounter(GameObject enemy)
	{
		if (currentState == EncounterState.INACTIVE)
		{
			currentState = EncounterState.ACTIVE;
			StartEncounter(enemy);
			GameDirector.instance.GetPlayer().GetComponent<Player>().AddFear();
		}
	}

    public void StopEncounter()
    {
		GameDirector.instance.StopEncounterMode();
		this.Cower();
		currentState = EncounterState.INACTIVE;
		GameDirector.instance.GetPlayer().GetComponent<Player>().RemoveFear();
		EncounterReset();
    }

    public void StartEncounter(GameObject enemy)
    {
		GameDirector.instance.StartEncounterMode();
		currentEnemy = enemy;
    }

//	public void InitializeSafetyLight()
//	{
//		safetyLight.GetComponentInChildren<SafetyLightController>().Initialize(this);
//		lightOn = true;
//		GameDirector.instance.GetPlayer().GetComponent<Player>().RemoveFear();
//	}

	public void KillSafetyLight()
	{
		safetyLight.GetComponentInChildren<SafetyLightController>().DisableSafetyLight();
		cooldown = true;
	}

	void LightCooldown()
	{
		lightTimer -= Time.deltaTime;
		if (lightTimer <= 0.0f)
		{
			lightTimer = 20.0f;
			cooldown = false;
			GameDirector.instance.LightReset();
		}
	}

	public EncounterState GetState()
	{
		return this.currentState;
	}

	public void AddToOvercomeCounter()
	{
		overcomeCounter++;
		if (overcomeCounter == 3)
		{
			ableToOvercome = true;
		}
	}

	public void SubtractFromOvercomeCounter()
	{
		if (overcomeCounter > 0)
		{
			overcomeCounter--;
		}
	}

	void EncounterReset()
	{
		overcomeCounter = 0;
		ableToOvercome = false;
		GameDirector.instance.GetPlayer().GetComponent<Player>().ResetEncounter();
	}

	public bool GetOvercomeStatus()
	{
		return ableToOvercome;
	}

	public void PlayerCanOvercome()
	{
		GameDirector.instance.GetPlayer().GetComponent<Player>().Overcome();
	}

	void GameOverTimer()
	{
		gameOverTimer -= Time.deltaTime;
		if (gameOverTimer <= 0.0f)
		{
			//inform GameDirector player dead
		}
	}

//	public bool CanUseToken()
//	{
//		if (currentState == EncounterState.ACTIVE && !cooldown)
//		{
//			return true;
//		}
//		return false;
//	}

	public void PlayerOvercame()
	{
		StopEncounter();
	}

	public void DestroyMe()
	{
		Destroy(currentEnemy);
	}

	public void Overpower()
	{
		currentEnemy.GetComponent<BasicEnemyController> ().Overpower ();
	}

	public void ResetOverpower()
	{
		currentEnemy.GetComponent<BasicEnemyController> ().ResetOverpower ();
	}

	public void NextOPStage()
	{
		currentEnemy.GetComponent<BasicEnemyController> ().NextOPStage ();
	}

	public void Cower()
	{
		currentEnemy.GetComponent<BasicEnemyController> ().Cower ();
	}
}
