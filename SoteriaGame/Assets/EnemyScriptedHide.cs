using UnityEngine;
using System.Collections;

public class EnemyScriptedHide : MonoBehaviour {
	
	private GameObject Player;
	private GameObject Enemy;
	private GameObject EnemyTarget;
	// Use this for initialization
	void Start () 
	{
		Player = GameObject.FindWithTag("Player");
		Enemy = GameObject.FindWithTag("Enemy");
		EnemyTarget = GameObject.FindWithTag("EnemyTarget");

		EnemyTarget.transform.position = new Vector3 ( EnemyTarget.transform.position.x,
		                                               Enemy.transform.position.y,
		                                               EnemyTarget.transform.position.z );
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Enemy != null)
		{
			Enemy.transform.position = new Vector3 (  Enemy.transform.position.x,
			                                          1.0f,
			                                          Enemy.transform.position.z );

			if (Enemy.transform.position.Equals(EnemyTarget.transform.position))
				Destroy(Enemy);
		}
	}

	
	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
			Player.GetComponent<Player>().HideDown();
			Debug.Log ("Enter");
			if (Enemy != null)
				Enemy.gameObject.GetComponent<NavMeshAgent>().SetDestination(EnemyTarget.transform.position);
		}
	}
	
	void OnTriggerStay(Collider Other)
	{
		if (Other.tag == "Player")
		{
			Debug.Log ("Stay");
			Player.GetComponent<Player>().HideIdle();
		}
	}
	
	void OnTriggerExit(Collider Other) 
	{ 
		if (Other.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Normal);
			Player.GetComponent<Player>().HideUp();
			Debug.Log ("Exit");
		}
	}
}
