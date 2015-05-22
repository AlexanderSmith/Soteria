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
	bool cooldown = false;
	float timer = 20.0f;
	int overcomeCounter = 0;
	bool ableToOvercome = false;

	public enum EncounterState
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
		if (cooldown)
		{
			LightCooldown();
		}
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
			GameDirector.instance.GetPlayer().GetComponentInChildren<Qte_Handle>().AddFear();
		}
		safetyLight.GetComponentInChildren<SafetyLightController> ().CurrentEnemy(enemy);
	}

    public void StopEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StopEncounterMode();
		lightOn = false;
		currentState = EncounterState.Inactive;
		GameDirector.instance.GetPlayer().GetComponentInChildren<Qte_Handle>().RemoveFear();
		EncounterReset();
    }

    public void StartEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StartEncounterMode(cooldown);
    }

	public void InitializeSafetyLight()
	{
		safetyLight.GetComponentInChildren<SafetyLightController>().Initialize(this);
		lightOn = true;
		currentState = EncounterState.ActiveLight;
		GameDirector.instance.GetPlayer().GetComponentInChildren<Qte_Handle>().RemoveFear();
	}

	public void KillSafetyLight()
	{
		safetyLight.GetComponentInChildren<SafetyLightController>().DisableSafetyLight();
		cooldown = true;
		//Debug.Log ("Killing light");
	}

	void LightCooldown()
	{
		timer -= Time.deltaTime;
		if (timer <= 0.0f)
		{
			timer = 20.0f;
			cooldown = false;
			this.gameObject.GetComponent<GameDirector>().LightReset();
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
			overcomeCounter = 0;
		}
	}

	void EncounterReset()
	{
		overcomeCounter = 0;
		ableToOvercome = false;
		GameDirector.instance.GetPlayer().GetComponentInChildren<Qte_Handle>().ResetOvercome();
	}

	public bool GetOvercomeStatus()
	{
		return ableToOvercome;
	}

	public void PlayerCanOvercome()
	{
		GameDirector.instance.GetPlayer().GetComponentInChildren<Qte_Handle>().Overcome();
	}
}
