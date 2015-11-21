using UnityEngine;
using System.Collections;

public class FailedMusic : MonoBehaviour
{
	public GameObject oMalleyPrefab;
	public Transform oMalleySpawnLoc;

	void Awake()
	{
		if (GameDirector.instance.GetFirstTimeMusic())
		{
			GameObject oMalley = Instantiate (oMalleyPrefab, oMalleySpawnLoc.position, oMalleySpawnLoc.rotation) as GameObject;
			GameDirector.instance.OMalleyPuzzleDone(GameDirector.instance.GetFirstTimeMusic());
		}
	}
}