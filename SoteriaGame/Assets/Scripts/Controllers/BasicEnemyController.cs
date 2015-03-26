using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	private GameObject target;
    public float lookAtDistance;
    public float attackRange;
	public float overwhelmRange;

    BasicAggroSystem aggroManager;

	// Use this for initialization
	void Awake() {
		target = GameObject.Find ("Player");
        aggroManager = new BasicAggroSystem();
	}
	
	// Update is called once per frame
	void Update () 
	{
        aggroManager.AggroCheckAndBasicMove(lookAtDistance, attackRange, overwhelmRange, target.transform, this.transform);
	}
}
