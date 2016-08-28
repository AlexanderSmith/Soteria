using UnityEngine;
using System.Collections;

public class FailedMusic : MonoBehaviour
{
	private GameObject _oMalleyPrefab;
	public Transform oMalleySpawnLoc;
	public GameObject nextObjective;

	void Awake()
	{
		_oMalleyPrefab = GameObject.Find ("O'MalleyFailedMusic");
		_oMalleyPrefab.transform.position = oMalleySpawnLoc.position;
	}

	void Start()
	{
		if (GameDirector.instance.GetGamePhase() < 4 && GameDirector.instance.GetFirstTimeMusic())
		{
			GameDirector.instance.OMalleyMusicPuzzleDone();
		}
		else
		{
			_oMalleyPrefab.SetActive(false);
			GameDirector.instance.ChangeObjective(nextObjective);
		}
	}
}