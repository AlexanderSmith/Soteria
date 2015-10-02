using UnityEngine;
using System.Collections;

public class HidePlayer : MonoBehaviour {

	private GameObject Player;
	// Use this for initialization
	void Start () 
	{
		Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
			Player.GetComponent<Player>().HideDown();
		    Debug.Log ("Enter");
		}
	}
	
	void OnTriggerStay(Collider Other)
	{
		if (Other.gameObject.tag == "Player")
		{
		    Debug.Log ("Stay");
			Player.GetComponent<Player>().HideIdle();
		}
	}

	void OnTriggerExit(Collider Other) 
	{ 
		if (Other.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeGameState(GameStates.Normal);
			Player.GetComponent<Player>().HideUp();
		    Debug.Log ("Exit");
		}
	}
}
