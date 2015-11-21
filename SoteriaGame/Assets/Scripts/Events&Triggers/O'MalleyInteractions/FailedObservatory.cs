using UnityEngine;
using System.Collections;

public class FailedObservatory : MonoBehaviour
{
	public GameObject oMalleyPrefab;
	public Transform oMalleySpawnLoc;
	
	void Awake()
	{
		if (GameDirector.instance.GetFirstTimeObservatory())
		{
			GameObject oMalley = Instantiate (oMalleyPrefab, oMalleySpawnLoc.position, oMalleySpawnLoc.rotation) as GameObject;
			GameDirector.instance.OMalleyPuzzleDone(GameDirector.instance.GetFirstTimeObservatory());
		}
	}
}