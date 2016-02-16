using UnityEngine;
using System.Collections;

public class ObsPuzzleSceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform playerSpawnLoc;
	
	private GameObject _controller;
	private GameObject _intro;
	
	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		GameObject player = Instantiate(playerPrefab, playerSpawnLoc.position, playerSpawnLoc.rotation) as GameObject;
		GameDirector.instance.InitializePlayer();

		// Testing
//		GameDirector.instance.SuitWorn();
//		GameDirector.instance.FirstTimeObservatoryPuzzle();
	}
	
	void Start()
	{
		GameDirector.instance.ChangeObjective(GameObject.Find("KeyPiece"));
		_controller = GameObject.Find("ObsPuzzleController");
		_controller.GetComponent<ObservatoryPuzzleController>().Initialize();
	}

//	void OnTriggerEnter(Collider player)
//	{
//		if (player.gameObject.tag == "Player" && !GameDirector.instance.GetObsActivated() && GameDirector.instance.GetGameState() != GameStates.Suit)
//		{
////			GameDirector.instance.GetPlayer().PlayerActionPause();
////			GameDirector.instance.SetupDialogue("AnaEnteringObservPuzzFirstTime");
////			GameDirector.instance.StartDialogue();
//		}
//		else
//		{
//			this.gameObject.GetComponent<BoxCollider>().enabled = false;
//			//GameDirector.instance.GetPlayer().PlayerActionNormal();
//		}
//	}
//
//	void OnTriggerStay(Collider player)
//	{
//		if (player.gameObject.tag == "Player")
//		{
//			if (!GameDirector.instance.isDialogueActive())
//			{
//				this.gameObject.GetComponent<BoxCollider>().enabled = false;
//				GameDirector.instance.GetPlayer().PlayerActionNormal();
//			}
//		}
//	}
}