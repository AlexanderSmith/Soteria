﻿using UnityEngine;
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
	private GameObject currentEnemy;

	public float lookAtDistance = 45.0f;
	public float attackRange = 35.0f;
	public float overwhelmRange = 15.0f;
	public float gameOverTimer = 75.0f;
	private float hiddenTileDuration;
	public float hiddenTileTimer = 5.0f;

	private int overcomeCounter = 0;

	private bool lightOn = false;
	private bool cooldown = false;
	private bool ableToOvercome = false;

	EncounterState currentState;

    // Use this for initialization
    void Start()
    {
		this.hiddenTileDuration = this.hiddenTileTimer;
    }

    // Update is called once per frame
    void Update()
    {
//		if (cooldown)
//		{
//			LightCooldown();
//		}
		if (this.currentState == EncounterState.ACTIVE)
		{
			this.GameOverTimer();
		}
    }

    public void Initialize()
    {
		this.enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		this.LinkToEnemy();
		this.currentState = EncounterState.INACTIVE;
    }

	void LinkToEnemy()
	{
		foreach (GameObject enemy in enemies) 
		{
			Debug.Log(enemy.name);
			enemy.GetComponent<BasicEnemyController>().Initialize(this);
		}
	}

	public void CheckPlayerDistance(GameObject enemy, bool inDead)
	{
		float distance = enemy.GetComponent<BasicEnemyController>().GetDistance();
		if (!inDead && GameDirector.instance.GetGameState() != GameStates.Hidden && GameDirector.instance.GetGameState() != GameStates.HiddenTile)
		{
			if (distance <= overwhelmRange)
			{
				this.Encounter(enemy);
				//Debug.Log ("Distance" + enemy.GetComponent<BasicEnemyController>().GetDistance());
				enemy.GetComponent<BasicEnemyController>().OverwhelmPlayer();
				this.GameOverTimer();
			}
			else if (distance <= attackRange)
			{
				enemy.GetComponent<BasicEnemyController>().ChasePlayer();
			}
			else if (distance <= lookAtDistance)
			{
				enemy.GetComponent<BasicEnemyController>().LookAtPlayer();
			}
			else
			{
				enemy.GetComponent<BasicEnemyController>().Unaware();
			}
		}
		else if (GameDirector.instance.GetGameState() == GameStates.HiddenTile)
		{
			if (distance <= lookAtDistance)
			{
				enemy.GetComponent<BasicEnemyController>().LookAtPlayer();
			}
			this.TileTimer();
		}
		else if (GameDirector.instance.GetGameState() == GameStates.Hidden)
		{
			enemy.GetComponent<BasicEnemyController>().Unaware();
		}
	}

	public void Encounter(GameObject enemy)
	{
		if (this.currentState == EncounterState.INACTIVE)
		{
			this.currentState = EncounterState.ACTIVE;
			this.StartEncounter(enemy);
			GameDirector.instance.GetPlayer().GetComponent<Player>().AddFear();
		}
	}

    public void StopEncounter()
    {
		this.Cower();
		this.currentState = EncounterState.INACTIVE;
		GameDirector.instance.GetPlayer().GetComponent<Player>().RemoveFear();
		GameDirector.instance.StopEncounterMode();
		this.EncounterReset();
    }

    public void StartEncounter(GameObject enemy)
    {
		GameDirector.instance.StartEncounterMode();
		this.currentEnemy = enemy;
    }

//	public void InitializeSafetyLight()
//	{
//		safetyLight.GetComponentInChildren<SafetyLightController>().Initialize(this);
//		lightOn = true;
//		GameDirector.instance.GetPlayer().GetComponent<Player>().RemoveFear();
//	}

//	public void KillSafetyLight()
//	{
//		safetyLight.GetComponentInChildren<SafetyLightController>().DisableSafetyLight();
//		cooldown = true;
//	}

//	void LightCooldown()
//	{
//		lightTimer -= Time.deltaTime;
//		if (lightTimer <= 0.0f)
//		{
//			lightTimer = 20.0f;
//			cooldown = false;
//			GameDirector.instance.LightReset();
//		}
//	}

	public EncounterState GetState()
	{
		return this.currentState;
	}

	public void AddToOvercomeCounter()
	{
		this.overcomeCounter++;
		if (this.overcomeCounter == 3)
		{
			this.ableToOvercome = true;
		}
	}

	public void SubtractFromOvercomeCounter()
	{
		if (this.overcomeCounter > 0)
		{
			this.overcomeCounter--;
		}
	}

	void EncounterReset()
	{
		this.overcomeCounter = 0;
		this.ableToOvercome = false;
		GameDirector.instance.GetPlayer().GetComponent<Player>().ResetEncounter();
	}

	public bool GetOvercomeStatus()
	{
		return this.ableToOvercome;
	}

	public void PlayerCanOvercome()
	{
		GameDirector.instance.GetPlayer().GetComponent<Player>().Overcome();
	}

	void GameOverTimer()
	{
		this.gameOverTimer -= Time.deltaTime;
		if (this.gameOverTimer <= 0.0f)
		{
			GameDirector.instance.StopEncounterMode();
			this.gameObject.AddComponent<LevelManager>().SetActiveLevel("HarborNoSwarm");
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
		this.StopEncounter();
	}

	public void DestroyMe()
	{
		Destroy(currentEnemy);
	}

	public void Overpower()
	{
		this.currentEnemy.GetComponent<BasicEnemyController> ().Overpower ();
	}

	public void ResetOverpower()
	{
		this.currentEnemy.GetComponent<BasicEnemyController> ().ResetOverpower ();
	}

	public void NextOPStage()
	{
		this.currentEnemy.GetComponent<BasicEnemyController> ().NextOPStage ();
	}

	public void Cower()
	{
		this.currentEnemy.GetComponent<BasicEnemyController> ().Cower ();
	}

	private void TileTimer()
	{
		this.hiddenTileDuration -= Time.deltaTime;
		if (this.hiddenTileDuration <= 0)
		{
			this.hiddenTileDuration = this.hiddenTileTimer;
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
		}
	}
}
