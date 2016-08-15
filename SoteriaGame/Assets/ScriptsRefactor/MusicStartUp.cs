using UnityEngine;
using System.Collections;

public class MusicStartUp : MonoBehaviour
{
	public GameObject music1Load;
	public GameObject music2Load;
	public GameObject music3Load;
	public GameObject music4Load;

	void Awake()
	{
//		GameDirector.instance.AddGamePhase();
//		GameDirector.instance.AddGamePhase();
//		GameDirector.instance.MusicPuzzleDefeated();
//		GameDirector.instance.CanFightTrue();
//		GameDirector.instance.CompassTrue();

		switch (GameDirector.instance.GetGamePhase())
		{
		case 1:
			music2Load.SetActive(false);
			music3Load.SetActive(false);
			music4Load.SetActive(false);
			break;
		case 2:
			music1Load.SetActive(false);
			music3Load.SetActive(false);
			music4Load.SetActive(false);
			break;
		case 3:
			music1Load.SetActive(false);
			music2Load.SetActive(false);
			music4Load.SetActive(false);
			break;
		case 4:
			music1Load.SetActive(false);
			music2Load.SetActive(false);
			music3Load.SetActive(false);
			break;
		}
	}
}