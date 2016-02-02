using UnityEngine;
using System.Collections;

public class Hub1SceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform statueSpawnLoc;
	public Transform musicSpawnLoc;

	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		if (GameDirector.instance.GetFromMusicDistrict())
		{
			GameObject player = Instantiate(playerPrefab, musicSpawnLoc.position, musicSpawnLoc.rotation) as GameObject;
			GameDirector.instance.ResetFromMusicDistrict();
			this.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			GameObject player = Instantiate(playerPrefab, statueSpawnLoc.position, statueSpawnLoc.rotation) as GameObject;
		}

		GameDirector.instance.InitializePlayer();
	}
}