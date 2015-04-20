using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	private GameObject target;
    public float lookAtDistance;
    public float attackRange;
	public float overwhelmRange;

    BasicAggroSystem aggroManager;

	private bool staystill; 


	// Use this for initialization
	void Awake() {
		target = GameObject.Find ("Player");
        aggroManager = new BasicAggroSystem();
		staystill = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!staystill)
		{
        	aggroManager.AggroCheckAndBasicMove(lookAtDistance, attackRange, overwhelmRange, target.transform, this.transform);
		}
	}

	public void EndEncounter (bool status)
	{
		staystill = status;
	}
}
