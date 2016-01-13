using UnityEngine;
using System.Collections;

public class PuppetPuzzleSceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform playerSpawnLoc;

	private GameObject _controller;

	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		GameObject player = Instantiate(playerPrefab, playerSpawnLoc.position, playerSpawnLoc.rotation) as GameObject;
		GameDirector.instance.InitializePlayer();
		
		// Testing
		//GameDirector.instance.SuitWorn();
		//GameDirector.instance.FirstTimeObservatoryPuzzle();
	}

	void Start()
	{
		GameDirector.instance.ChangeObjective(GameObject.Find("KeyPiece"));
		_controller = GameObject.Find("PuppetPuzzleController");
		_controller.GetComponent<PuppetPuzzleController>().Initialize();
	}
}