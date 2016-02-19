using UnityEngine;
using System.Collections;

public class CheckObservatoryDefeatedObjective : MonoBehaviour
{
	private GameObject _observatory;
	private GameObject _portToHub;
	
	void Awake()
	{
		this._observatory = GameObject.Find("Observatory");
		this._portToHub = GameObject.Find("PortToHub");
	}
	
	void Start()
	{
		if (!GameDirector.instance.IsMusicDefeated() || !GameDirector.instance.IsTheaterDefeated() || GameDirector.instance.IsObservatoryDefeated())
		{
			GameDirector.instance.ChangeObjective(this._portToHub);
		}
		else
		{
			GameDirector.instance.ChangeObjective(this._observatory);
		}
	}
}