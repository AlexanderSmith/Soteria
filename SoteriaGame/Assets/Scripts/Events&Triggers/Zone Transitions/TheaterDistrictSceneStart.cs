using UnityEngine;
using System.Collections;

public class TheaterDistrictSceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform districtSpawnLoc;
	public Transform puzzleSpawnLoc;
	
	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		if (GameDirector.instance.GetFromPuzzle())
		{
			GameObject player = Instantiate(playerPrefab, puzzleSpawnLoc.position, puzzleSpawnLoc.rotation) as GameObject;
			GameDirector.instance.InitializePlayer();
			GameDirector.instance.ResetFromPuzzle();
			this.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			GameObject player = Instantiate(playerPrefab, districtSpawnLoc.position, districtSpawnLoc.rotation) as GameObject;
			GameDirector.instance.InitializePlayer();
			GameDirector.instance.ResetFromTheaterDistrict();
		}
	}

//	void Start()
//	{
//		GameDirector.instance.CompassTrue();
//		GameDirector.instance.TheaterPuzzleDefeated();
//	}
}
