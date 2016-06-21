using UnityEngine;
using System.Collections;

public class TheaterStartUp : MonoBehaviour
{
	public GameObject theater1Load;
	public GameObject theater2Load;
	public GameObject theater3Load;
	public GameObject theater4Load;
	
	void Awake()
	{
//		GameDirector.instance.AddGamePhase();
		switch (GameDirector.instance.GetGamePhase())
		{
		case 1:
			theater2Load.SetActive(false);
			theater3Load.SetActive(false);
			theater4Load.SetActive(false);
			break;
		case 2:
			theater1Load.SetActive(false);
			theater3Load.SetActive(false);
			theater4Load.SetActive(false);
			break;
		case 3:
			theater1Load.SetActive(false);
			theater2Load.SetActive(false);
			theater4Load.SetActive(false);
			break;
		case 4:
			theater1Load.SetActive(false);
			theater2Load.SetActive(false);
			theater3Load.SetActive(false);
			break;
		}
	}
}