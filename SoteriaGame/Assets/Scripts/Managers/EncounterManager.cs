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
	private GameObject currentEnemy;

	private float gameOverTimer = 15.0f;
	private float hiddenTileDuration;
	public float hiddenTileTimer = 5.0f;

	private int overcomeCounter = 0;

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
		if (this.currentState == EncounterState.ACTIVE)
		{
			this.GameOverTimer();
		}
    }

    public void Initialize()
    {
		this.currentState = EncounterState.INACTIVE;
    }

	public void Encounter(GameObject enemy)
	{
		if (this.currentState == EncounterState.INACTIVE)
		{
			this.currentState = EncounterState.ACTIVE;
			this.StartEncounter(enemy);
			GameDirector.instance.GetPlayer().AddFear();
		}
	}

    public void StopEncounter()
    {
		this.Cower();
		this.currentState = EncounterState.INACTIVE;
		GameDirector.instance.GetPlayer().RemoveFear();
		GameDirector.instance.StopEncounterMode();
		this.EncounterReset();
    }

	public void StopEncounterFromToken()
	{
		this.currentState = EncounterState.INACTIVE;
		GameDirector.instance.GetPlayer().RemoveFear();
		GameDirector.instance.StopEncounterMode();
		this.EncounterReset();
	}

    public void StartEncounter(GameObject enemy)
    {
		GameDirector.instance.StartEncounterMode();
		this.currentEnemy = enemy;
    }

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
		this.currentState = EncounterState.INACTIVE;
		this.ResetGameOverTimer();
		GameDirector.instance.GetPlayer().ResetEncounter();
	}

	public bool GetOvercomeStatus()
	{
		return this.ableToOvercome;
	}

	public void PlayerCanOvercome()
	{
		GameDirector.instance.GetPlayer().Overcome();
	}

	private void GameOverTimer()
	{
		this.gameOverTimer -= Time.deltaTime;
		if (this.gameOverTimer <= 0.0f)
		{
			GameDirector.instance.GameOver();
			this.EncounterReset();
		}
	}

	public void ResetGameOverTimer()
	{
		this.gameOverTimer = 15.0f;
	}

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
		this.currentEnemy.GetComponent<BasicEnemyController>().Overpower ();
	}

	public void ResetOverpower()
	{
		this.currentEnemy.GetComponent<BasicEnemyController>().ResetOverpower ();
	}

	public void NextOPStage()
	{
		this.currentEnemy.GetComponent<BasicEnemyController>().NextOPStage ();
	}

	public void Cower()
	{
		this.currentEnemy.GetComponent<BasicEnemyController>().Cower();
	}

	public void TileTimer()
	{
		this.hiddenTileDuration -= Time.deltaTime;
		if (this.hiddenTileDuration <= 0)
		{
			this.hiddenTileDuration = this.hiddenTileTimer;
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
		}
	}

	// Lantern stun on enemies within range
	public void LanternUsed()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			if (Vector3.Distance(enemy.transform.position, GameDirector.instance.GetPlayer().transform.position) <= enemy.GetComponent<BasicEnemyController>().lookAtDistance)
			{
				if (enemy.GetComponent<BasicEnemyController>() != null)
				{
					enemy.GetComponent<BasicEnemyController>().Stun();
				}
				else
				{
					enemy.GetComponent<EyeballShadowCreatureController>().Stun();
				}
			}
		}
	}

	public void TokenUsed()
	{
		this.StopEncounterFromToken();
	}
}
