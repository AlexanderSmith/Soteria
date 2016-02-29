using UnityEngine;
using System.Collections;

public class CheckTheaterDefeatedObjective : MonoBehaviour
{
	private GameObject _puppetStore;
	private GameObject _portToHub;
	private GameObject _oMalleySewerProvoke;
	
	void Awake()
	{
		this._puppetStore = GameObject.Find("PuppetStore");
		this._portToHub = GameObject.Find("PortToHub");
		this._oMalleySewerProvoke = GameObject.Find("MalleyTheaterProvoke");
		this._oMalleySewerProvoke.SetActive(false);

		//Hacks for testing provoke
//		GameDirector.instance.TheaterPuzzleDefeated();
//		GameDirector.instance.SuitRemoved();
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
				case StatueCrumbleState.CRUMBLEONE:
					this._oMalleySewerProvoke.SetActive(true);
					GameDirector.instance.ChangeObjective(this._oMalleySewerProvoke);
					break;
				default:
					GameDirector.instance.ChangeObjective(this._portToHub);
					break;
				}
			}
			else
			{
				GameDirector.instance.ChangeObjective(this._puppetStore);
			}
		}
		else
		{
			GameDirector.instance.ChangeObjective(null);
		}

		if (GameDirector.instance.IsTheaterDefeated())
		{
			this._oMalleySewerProvoke.SetActive(true);
		}
	}
}