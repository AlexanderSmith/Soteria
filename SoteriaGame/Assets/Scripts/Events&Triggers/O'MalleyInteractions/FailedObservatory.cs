using UnityEngine;
using System.Collections;

public class FailedObservatory : MonoBehaviour
{
	private GameObject _oMalleyPrefab;
	public Transform oMalleySpawnLoc;
	
	void Awake()
	{
		_oMalleyPrefab = GameObject.Find ("O'MalleyFailedObservatory");
		_oMalleyPrefab.transform.position = oMalleySpawnLoc.position;
		// Testing
		//GameDirector.instance.FirstTimeObservatoryPuzzle();
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
			GameDirector.instance.ChangeObjective(GameObject.Find("TailorDialogueLocation"));
		}
	}
}