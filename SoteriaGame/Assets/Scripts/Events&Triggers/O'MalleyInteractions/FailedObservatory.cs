using UnityEngine;
using System.Collections;

public class FailedObservatory : MonoBehaviour
{
	private GameObject _oMalleyPrefab;
	public Transform oMalleySpawnLoc;
	//public GameObject tailorLight;
	public GameObject nextObjective;
	
	void Awake()
	{
		_oMalleyPrefab = GameObject.Find ("O'MalleyFailedObservatory");
		_oMalleyPrefab.transform.position = oMalleySpawnLoc.position;
		//tailorLight.GetComponent<Light> ().enabled = false;
		// Testing
//		GameDirector.instance.FirstTimeObservatoryPuzzle();
	}
	
	void Start()
	{
		if (GameDirector.instance.GetFirstTimeObservatory())
		{
			GameDirector.instance.OMalleyObservatoryPuzzleDone();
		}
		else
		{
			_oMalleyPrefab.SetActive(false);
			if (Application.loadedLevelName != "HUBPass1")
			{
				GameDirector.instance.ChangeObjective(nextObjective);
			}
		}
	}
}