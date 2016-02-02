using UnityEngine;
using System.Collections;

public class Hub3SceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform statueSpawnLoc;
	public Transform musicSpawnLoc;
	public Transform theaterSpawnLoc;
	public Transform observatorySpawnLoc;
	
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
		else if (GameDirector.instance.GetFromObservatoryDistrict())
		{
			GameObject player = Instantiate(playerPrefab, observatorySpawnLoc.position, observatorySpawnLoc.rotation) as GameObject;
			GameDirector.instance.ResetFromObservatoryDistrict();
			this.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			GameObject player = Instantiate(playerPrefab, statueSpawnLoc.position, statueSpawnLoc.rotation) as GameObject;
		}
		
		GameDirector.instance.InitializePlayer();
	}
}
