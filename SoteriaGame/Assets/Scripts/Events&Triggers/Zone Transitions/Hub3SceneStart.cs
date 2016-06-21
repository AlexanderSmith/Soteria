using UnityEngine;
using System.Collections;

public class Hub3SceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform statueSpawnLoc;
	public Transform musicSpawnLoc;
	public Transform theaterSpawnLoc;
	public Transform observatorySpawnLoc;

	public GameObject sotStatue;
	public GameObject sotStatueCrumble1;
	public GameObject sotStatueCrumble2;
	private Material[] crumble2Mats;
	
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

		// Testing statue final crumble code
//		GameDirector.instance.StatueCrumbleThree();

		switch(GameDirector.instance.GetStatueCrumble())
		{
		case StatueCrumbleState.WHOLE:
			sotStatueCrumble1.SetActive(false);
			sotStatueCrumble2.SetActive(false);
			break;
		case StatueCrumbleState.CRUMBLEONE:
			sotStatue.SetActive(false);
			sotStatueCrumble2.SetActive(false);
			break;
		case StatueCrumbleState.CRUMBLETWO:
		case StatueCrumbleState.CRUMBLETHREE:
			sotStatue.SetActive(false);
			sotStatueCrumble1.SetActive(false);
			crumble2Mats = sotStatueCrumble2.GetComponent<MeshRenderer>().materials;
			foreach (Material mat in crumble2Mats)
			{
				mat.color = Color.gray;
			}
			break;
		}
	}
}