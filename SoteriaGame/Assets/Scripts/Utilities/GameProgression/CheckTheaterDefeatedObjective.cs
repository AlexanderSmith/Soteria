using UnityEngine;
using System.Collections;

public class CheckTheaterDefeatedObjective : MonoBehaviour
{
	private GameObject _puppetStore;
	private GameObject _portToHub;
	
	void Awake()
	{
		this._puppetStore = GameObject.Find("PuppetStore");
		this._portToHub = GameObject.Find("PortToHub");
	}
	
	void Start()
	{
		if (GameDirector.instance.IsTheaterDefeated())
		{
			GameDirector.instance.ChangeObjective(this._portToHub);
		}
		else
		{
			GameDirector.instance.ChangeObjective(this._puppetStore);
		}
	}
}