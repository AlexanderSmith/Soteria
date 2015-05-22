using UnityEngine;
using System.Collections;

public class EncounterMovementController : MonoBehaviour {

    //public SceneFadeInOut fearFadeOutTextureController;
    public float playerRotation;
    public float forcedRotation;
    public float gameOverTimer;
    public int Coins;

    Quaternion overwhelmedRotation;
    Movement myMovementComponents;

    GameObject enemyAttacker;

    private Quaternion enemyRoation, directionNeededToOverCome;
    private int overComingCounters = 0;
	private StateManager gameStateController;

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

	public void Initialize(StateManager stateMan)
	{
		gameStateController = stateMan;
	}

    void Awake() {
    }
	
	// Update is called once per frame
	void Update () { }

//    void OnMouseDown()
//    {
//        if (this.enabled)
//        {
//            Debug.Log("Mouse Down In Encounter");
//            this.transform.Rotate(Vector3.up, playerRotation);
//        }
//    }

    public void Overwhelm(Transform enemy, bool lightOn)
    {
//		Debug.Log ("TryingToOverwhelm " + gameStateController.GameState());
//		enemyAttacker = enemy.gameObject;
//        if (currentState != EncounterState.Overwhelmed && !lightOn)
//        {
//            Debug.Log("OV");
//            this.GetComponent<PCController>().EnableEncounterMovement();
////            enemyRoation = enemy.rotation;
////
////            overwhelmedRotation = enemy.rotation;
////
////            directionNeededToOverCome = Quaternion.LookRotation(transform.position - enemy.position, Vector3.forward);
////            directionNeededToOverCome.x = 0.0f;
////            directionNeededToOverCome.z = 0.0f;
////
////            
////            this.transform.rotation = overwhelmedRotation;
//            //Debug.Log(this.transform.rotation);
//            currentState = EncounterState.Overwhelmed;
//        }
    }

//    public void OverCome()
//    {
//        gameObject.GetComponent<PCController>().EnableStandardMovement();
//        //Eventually will call enemy death script funtion mostlikely so there is a nice disipation and stuff. 
////        Destroy(enemyAttacker);
//        currentState = EncounterState.Normal;
//    }
//
//    public void CheckEscape()
//    {
//        enemyAttacker.GetComponent<BasicEnemyController>().EndEncounter(true);
//        currentState = EncounterState.Free;
//		this.GetComponent<PCController>().EnableStandardMovement();
//        //StartCoroutine(EnableEncounter());
//    }

    public EncounterState GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(EncounterState e)
    {
        currentState = e;
    }
}
