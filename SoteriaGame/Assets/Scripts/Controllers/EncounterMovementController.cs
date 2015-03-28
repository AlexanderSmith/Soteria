using UnityEngine;
using System.Collections;

public class EncounterMovementController : MonoBehaviour {

    public int numSoteriaStatues;
    public Object soteriaStatuePrefab;
    public float playerRotation;
    public float forcedRotation;
    public float gameOverTimer;
    public int Coins;

    Quaternion overwhelmedRotation;
    Movement myMovementComponents;

    GameObject enemyAttacker;

    private Quaternion enemyRoation, directionNeededToOverCome;
    private int overComingCounters = 0;

    enum State
    {
        Normal,
        Overwhelmed,
        Dead,
        Free
    };
    State currentState;
	// Use this for initialization
	void Start () {
	    currentState = State.Normal;
	}

    void Awake() {
        currentState = State.Normal;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentState == State.Overwhelmed)
        {
            GameOverTimer();

            //float angle = Quaternion.Angle(this.transform.rotation, overwhelmedRotation);

            if (this.transform.rotation != overwhelmedRotation && !Input.GetKeyDown(KeyCode.Space))
            {
                this.transform.Rotate(Vector3.up, forcedRotation);
            //    Debug.Log("TOO LARGE -- TOO LARGE");
            //    this.transform.RotateAround(this.transform.position, Vector3.up, forcedRotation);
            }

            //GameObject Enemy = GameObject.Find("Enemy");

            //angle = 10;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Debug.DrawRay(this.transform.position, ray.direction);
            Debug.Log(Vector3.Dot(this.transform.forward, enemyAttacker.transform.forward));
            if (Vector3.Dot(this.transform.forward, enemyAttacker.transform.forward) < -0.95f && Input.GetKeyDown(KeyCode.Space))//this.transform.rotation == directionNeededToOverCome)// Physics.Raycast(ray, out hit, 100))
            {
                overComingCounters++;
                if(overComingCounters > 50)
               // Debug.Log(hit.collider.name);
               // if (hit.collider.name == "Enemy")
               // {
                OverCome();
                    
               // }
            }
        }

        if (currentState == State.Free)
        {
            Vector3 speed = new Vector3(0.2f, 0.0f, 0.2f);
            speed.x *= this.transform.forward.x;
            speed.z *= this.transform.forward.z;

            this.transform.position += (speed);
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
        if (currentState != State.Overwhelmed)
        {
            enemyRoation = enemy.rotation;

            overwhelmedRotation = enemy.rotation;

            directionNeededToOverCome = Quaternion.LookRotation(transform.position - enemy.position, Vector3.forward);
            directionNeededToOverCome.x = 0.0f;
            directionNeededToOverCome.z = 0.0f;

            enemyAttacker = enemy.gameObject;
            this.transform.rotation = overwhelmedRotation;
            //Debug.Log(this.transform.rotation);
            currentState = State.Overwhelmed;
            CheckEscape();
        }
    }

    void OverCome()
    {
        gameObject.GetComponent<PCController>().EnableStandardMovement();
        //Eventually will call enemy death script funtion mostlikely so there is a nice disipation and stuff. 
        Destroy(enemyAttacker);
        currentState = State.Normal;
    }

    void CheckEscape()
    {
        if (numSoteriaStatues > 0)
        {
            numSoteriaStatues -= 1;
            //Create Soteria Statue prefab if player has statues in inventory
            Vector3 normal = this.transform.forward;
            normal.Normalize();
            //GameObject statue = Instantiate(soteriaStatuePrefab,
            //                                new Vector3(10 * normal.x + this.transform.position.x, this.transform.position.y, 10 * normal.z + this.transform.position.z),
            //                                this.transform.rotation) as GameObject;
            //Debug.Log("normal " + normal);
        }
    }

    void GameOverTimer()
    {
        gameOverTimer -= Time.deltaTime;
        if (gameOverTimer <= 0)
        {
            overwhelmedRotation = enemyRoation;
            this.transform.rotation = overwhelmedRotation;
            GameObject Enemy = GameObject.Find("Enemy");
            Enemy.gameObject.SendMessage("EndEncounter", true);
            this.currentState = State.Dead;
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
            currentState = State.Normal;
            GameObject Statue = GameObject.Find("SoteriaStatue(Clone)");
            if (Statue != null)
                Destroy(Statue);

            gameOverTimer = 10;
        }
    }

//    void OnGUI()
//    {
//        Event e = Event.current;
//        if (e.button == 1 && e.isMouse)
//            UseCoin();
//    }
}
