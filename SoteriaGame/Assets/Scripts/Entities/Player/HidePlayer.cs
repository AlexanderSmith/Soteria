using UnityEngine;
using System.Collections;

public class HidePlayer : MonoBehaviour {

	//private GameObject Player;
	// Use this for initialization
	void Start () 
	{
		//Player = GameObject.FindWithTag("Player");
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
			GameDirector.instance.GetPlayer().PlayerActionHiding();
		    //Debug.Log ("Enter");
		}
	}
	
//	void OnTriggerStay(Collider Other)
//	{
//		if (Other.gameObject.tag == "Player")
//		{
//		    //Debug.Log ("Stay");
//			GameDirector.instance.GetPlayer().HideIdle();
//		}
//	}

//	void OnTriggerExit(Collider Other) 
//	{ 
//		if (Other.gameObject.tag == "Player")
//		{
//			GameDirector.instance.ChangeGameState(GameStates.Normal);
//			GameDirector.instance.GetPlayer().HideUp();
//			//Debug.Log ("Exit");
//		}
//	}
}
