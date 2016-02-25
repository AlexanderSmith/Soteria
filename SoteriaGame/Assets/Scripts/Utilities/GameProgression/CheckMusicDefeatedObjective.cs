using UnityEngine;
using System.Collections;

public class CheckMusicDefeatedObjective : MonoBehaviour
{
	private GameObject _musicStore;
	private GameObject _portToHub;
	private GameObject _oMalleySewerProvoke;

	void Awake()
	{
		this._musicStore = GameObject.Find("MusicStore");
		this._portToHub = GameObject.Find("PortToHub");
		this._oMalleySewerProvoke = GameObject.Find("O'MalleyMusicProvoke");
		this._oMalleySewerProvoke.SetActive(false);

		//Hacks for testing provoke
		GameDirector.instance.MusicPuzzleDefeated();
		GameDirector.instance.SuitRemoved();
	}

	void Start()
	{
		if (GameDirector.instance.GetCompass())
		{
			if (GameDirector.instance.IsMusicDefeated())
			{
				switch (GameDirector.instance.GetStatueCrumble())
				{
				case StatueCrumbleState.WHOLE:
					this._oMalleySewerProvoke.SetActive(true);
					GameDirector.instance.ChangeObjective(this._oMalleySewerProvoke);
					break;
				default:
					GameDirector.instance.ChangeObjective(this._portToHub);
					break;
				}
//				if (GameDirector.instance.GetStatueCrumble() != StatueCrumbleState.CRUMBLEONE)
//				{
//					this._oMalleySewerProvoke.SetActive(true);
//					GameDirector.instance.ChangeObjective(this._oMalleySewerProvoke);
//				}
//				else
//				{
//					GameDirector.instance.ChangeObjective(this._portToHub);
//				}
			}
			else
			{
				GameDirector.instance.ChangeObjective(this._musicStore);
			}
		}
		else
		{
			GameDirector.instance.ChangeObjective(null);
		}
		if (GameDirector.instance.IsMusicDefeated())
		{
			this._oMalleySewerProvoke.SetActive(true);
		}
	}
}