using UnityEngine;
using System.Collections;

public class FailedTheater : MonoBehaviour
{
	private GameObject _oMalleyPrefab;
	public Transform oMalleySpawnLoc;
	
	void Awake()
	{
		_oMalleyPrefab = GameObject.Find ("O'MalleyFailedTheater");
		_oMalleyPrefab.transform.position = oMalleySpawnLoc.position;
	}
	
	void Start()
	{
		if (GameDirector.instance.GetFirstTimeTheater())
		{
			GameDirector.instance.OMalleyTheaterPuzzleDone();
		}
		else
		{
			_oMalleyPrefab.SetActive(false);
			if (GameDirector.instance.GetGameState() != GameStates.Suit && GameDirector.instance.IsTutorialComplete())
			{
				GameDirector.instance.ChangeObjective(GameObject.Find("HubToObservatory"));
			}
			else
			{
				GameDirector.instance.ChangeObjective(GameObject.Find ("HubToMusic"));
			}
		}
	}
}