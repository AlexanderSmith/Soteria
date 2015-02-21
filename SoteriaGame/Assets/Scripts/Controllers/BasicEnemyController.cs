using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

    public Transform target;
    public float lookAtDistance;
    public float attackRange;

    BasicAggroSystem aggroManager;
	// Use this for initialization
	void Start () {
        aggroManager = new BasicAggroSystem();
	}
	
	// Update is called once per frame
	void Update () {
        aggroManager.AggroCheckAndBasicMove(lookAtDistance, attackRange, target, transform);
	}
}
