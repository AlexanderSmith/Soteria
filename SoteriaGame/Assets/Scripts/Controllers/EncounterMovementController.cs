using UnityEngine;
using System.Collections;

public class EncounterMovementController : MonoBehaviour {

    public int numSoteriaStatues;
    public Object soteriaStatuePrefab;
    public SceneFadeInOut fearFadeOutTextureController;
    public float playerRotation;
    public float forcedRotation;
    public float gameOverTimer;
    public int Coins;

    Quaternion overwhelmedRotation;
    Movement myMovementComponents;

    GameObject enemyAttacker;

    private Quaternion enemyRoation, directionNeededToOverCome;
    private int overComingCounters = 0;

    public enum EncounterState
    {
        Normal,
        Hidden,
        Overwhelmed,
        Dead,
        Free
    };
    EncounterState currentState;
	// Use this for initialization
	void Start () {
        currentState = EncounterState.Normal;
	}

    void Awake() {
    }
	
	// Update is called once per frame
	void Update () {
        if (currentState == EncounterState.Overwhelmed)
        {
            GameOverTimer();

            if (this.transform.rotation != overwhelmedRotation && !Input.GetKeyDown(KeyCode.Space) && overComingCounters < 15)
            {
                this.transform.Rotate(Vector3.up, forcedRotation);
            }

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Debug.DrawRay(this.transform.position, ray.direction);
            if (Vector3.Dot(this.transform.forward, enemyAttacker.transform.forward) < -0.95f && Input.GetKeyDown(KeyCode.Space))//this.transform.rotation == directionNeededToOverCome)// Physics.Raycast(ray, out hit, 100))
            {
                overComingCounters++;
                if (overComingCounters > 30)
                {
                    OverCome();
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (this.enabled)
        {
            Debug.Log("Mouse Down In Encounter");
            this.transform.Rotate(Vector3.up, playerRotation);
        }
    }

    public void Overwhelm(Transform enemy)
    {
        if (currentState != EncounterState.Overwhelmed)
        {
            Debug.Log("OV");
            this.GetComponent<PCController>().EnableEncounterMovement();
            enemyRoation = enemy.rotation;

            overwhelmedRotation = enemy.rotation;

            directionNeededToOverCome = Quaternion.LookRotation(transform.position - enemy.position, Vector3.forward);
            directionNeededToOverCome.x = 0.0f;
            directionNeededToOverCome.z = 0.0f;

            enemyAttacker = enemy.gameObject;
            this.transform.rotation = overwhelmedRotation;
            //Debug.Log(this.transform.rotation);
            currentState = EncounterState.Overwhelmed;
        }
    }

    void OverCome()
    {
        gameObject.GetComponent<PCController>().EnableStandardMovement();
        //Eventually will call enemy death script funtion mostlikely so there is a nice disipation and stuff. 
        Destroy(enemyAttacker);
        currentState = EncounterState.Normal;
    }

    public void CheckEscape()
    {
        enemyAttacker.GetComponent<BasicEnemyController>().EndEncounter(true);
        currentState = EncounterState.Free;
        StartCoroutine(EnableEncounter());
    }

    void GameOverTimer()
    {
        gameOverTimer -= Time.deltaTime;
        fearFadeOutTextureController.FadeToBlack();
        if (gameOverTimer <= 0)
        {
            overwhelmedRotation = enemyRoation;
            this.transform.rotation = overwhelmedRotation;
            GameObject Enemy = GameObject.Find("Enemy");
            Enemy.gameObject.SendMessage("EndEncounter", true);
            this.currentState = EncounterState.Dead;
			PlayerOverwhelmed();
        }
    }

    void PlayerOverwhelmed()
    {
        Application.LoadLevel("TileEventSystem");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals("SoteriaStatue(Clone)"))
        {
            PlayerOverwhelmed();
        }

        if (other.gameObject.name.Equals("Enemy"))
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<PCController>().EnableStandardMovement();
            GameObject Statue = GameObject.Find("SoteriaStatue(Clone)");
            if (Statue != null)
                Destroy(Statue);

            gameOverTimer = 10;
        }
    }
    void UseCoin()
    {
        if (Coins > 0)
        {
            GameObject Enemy = GameObject.Find("Enemy");
            if (Enemy != null)
                Destroy(Enemy.gameObject);
            gameObject.GetComponent<PCController>().EnableStandardMovement();
            currentState = EncounterState.Normal;
            GameObject Statue = GameObject.Find("SoteriaStatue(Clone)");
            if (Statue != null)
                Destroy(Statue);

            gameOverTimer = 10;
        }
    }

    IEnumerator EnableEncounter()
    {
        yield return new WaitForSeconds(3f);

        enemyAttacker.GetComponent<BasicEnemyController>().EndEncounter(false);
    }

    public EncounterState GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(EncounterState e)
    {
        currentState = e;
    }
}
