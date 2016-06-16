using UnityEngine;
using System.Collections;

public class Hub2SceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform statueSpawnLoc;
	public Transform musicSpawnLoc;
	public Transform theaterSpawnLoc;
	
	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		if (GameDirector.instance.GetFromMusicDistrict())
		{
			GameObject player = Instantiate(playerPrefab, musicSpawnLoc.position, musicSpawnLoc.rotation) as GameObject;
			GameDirector.instance.ResetFromMusicDistrict();
			this.GetComponent<BoxCollider>().enabled = false;
		}
		else if (GameDirector.instance.GetFromTheaterDistrict())
		{
			GameObject player = Instantiate(playerPrefab, theaterSpawnLoc.position, theaterSpawnLoc.rotation) as GameObject;
			GameDirector.instance.ResetFromTheaterDistrict();
			this.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			GameObject player = Instantiate(playerPrefab, statueSpawnLoc.position, statueSpawnLoc.rotation) as GameObject;
		}

		GameDirector.instance.InitializePlayer();
	}
}
