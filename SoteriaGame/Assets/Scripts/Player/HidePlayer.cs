using UnityEngine;
using System.Collections;

public class HidePlayer : MonoBehaviour {

	//private GameObject Player;
	// Use this for initialization
	void Start () 
	{
		//Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider Other)
	{
		if (Other.gameObject.name.Equals("Player"))
		{
			GameDirector.instance.ChangeGameState(GameStates.Hidden);
		    Debug.Log ("Enter");
		}
	}
	
	void OnTriggerStay(Collider Other)
	{
		if (Other.gameObject.name.Equals("Player"))
		{
		    Debug.Log ("Stay");
		}
	}
	void OnTriggerExit(Collider Other) 
	{ 
		if (Other.gameObject.name.Equals("Player"))
		{
			GameDirector.instance.ChangeGameState(GameStates.Normal);
		    Debug.Log ("Exit");
		}
	}
}
