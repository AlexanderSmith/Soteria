using UnityEngine;
using System.Collections;

public class FailedMusic : MonoBehaviour
{
	private GameObject _oMalleyPrefab;
	public Transform oMalleySpawnLoc;

	void Awake()
	{
		_oMalleyPrefab = GameObject.Find ("O'MalleyFailedMusic");
		_oMalleyPrefab.transform.position = oMalleySpawnLoc.position;
	}

	void Start()
	{
		if (GameDirector.instance.GetFirstTimeMusic())
		{
			GameDirector.instance.OMalleyMusicPuzzleDone();
		}
		else
		{
			_oMalleyPrefab.SetActive(false);
		}
	}
}