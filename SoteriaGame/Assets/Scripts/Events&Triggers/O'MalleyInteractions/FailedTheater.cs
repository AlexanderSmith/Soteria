using UnityEngine;
using System.Collections;

public class FailedTheater : MonoBehaviour
{
	public GameObject oMalleyPrefab;
	public Transform oMalleySpawnLoc;
	
	void Awake()
	{
		if (GameDirector.instance.GetFirstTimeTheater())
		{
			GameObject oMalley = Instantiate (oMalleyPrefab, oMalleySpawnLoc.position, oMalleySpawnLoc.rotation) as GameObject;
			GameDirector.instance.OMalleyPuzzleDone(GameDirector.instance.GetFirstTimeTheater());
		}
	}
}