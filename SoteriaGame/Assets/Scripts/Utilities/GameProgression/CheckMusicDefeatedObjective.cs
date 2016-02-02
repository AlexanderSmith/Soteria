using UnityEngine;
using System.Collections;

public class CheckMusicDefeatedObjective : MonoBehaviour
{
	private GameObject _musicStore;
	private GameObject _portToHub;

	void Awake()
	{
		this._musicStore = GameObject.Find("MusicStore");
		this._portToHub = GameObject.Find("PortToHub");
	}

	void Start()
	{
		if (GameDirector.instance.IsMusicDefeated())
		{
			GameDirector.instance.ChangeObjective(this._portToHub);
		}
		else
		{
			GameDirector.instance.ChangeObjective(this._musicStore);
		}
	}
}