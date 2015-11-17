using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform playerSpawnLoc;
	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		GameObject player = Instantiate(playerPrefab, playerSpawnLoc.position, playerSpawnLoc.rotation) as GameObject;
		GameDirector.instance.InitializePlayer();
	}
}