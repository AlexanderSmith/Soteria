using UnityEngine;
using System.Collections;

public class EncounterMovementController : MonoBehaviour {

    //public SceneFadeInOut fearFadeOutTextureController;
    public float playerRotation;
    public float forcedRotation;
    public float gameOverTimer;
    public int Coins;

    Quaternion overwhelmedRotation;

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
	void Start () 
	{
        currentState = EncounterState.Normal;
	}

	public void Initialize(StateManager stateMan)
	{
		gameStateController = stateMan;
	}

    void Awake() {}

	void Update () 
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

//        if (currentState != EncounterState.Overwhelmed && !lightOn)

    public EncounterState GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(EncounterState e)
    {
        currentState = e;
    }
}
