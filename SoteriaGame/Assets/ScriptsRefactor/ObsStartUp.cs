using UnityEngine;
using System.Collections;

public class ObsStartUp : MonoBehaviour
{
	public GameObject obs1Load;
	public GameObject obs2Load;
	public GameObject obs3Load;
	public GameObject obs4Load;
	
	void Awake()
	{
//		GameDirector.instance.AddGamePhase();
//		GameDirector.instance.AddGamePhase();
//		GameDirector.instance.AddGamePhase();
//		GameDirector.instance.CanFightTrue();

		switch (GameDirector.instance.GetGamePhase())
		{
		case 1:
			obs2Load.SetActive(false);
			obs3Load.SetActive(false);
			obs4Load.SetActive(false);
			break;
		case 2:
			obs1Load.SetActive(false);
			obs3Load.SetActive(false);
			obs4Load.SetActive(false);
			break;
		case 3:
			obs1Load.SetActive(false);
			obs2Load.SetActive(false);
			obs4Load.SetActive(false);
			break;
		case 4:
			obs1Load.SetActive(false);
			obs2Load.SetActive(false);
			obs3Load.SetActive(false);
			break;
		}
	}
}