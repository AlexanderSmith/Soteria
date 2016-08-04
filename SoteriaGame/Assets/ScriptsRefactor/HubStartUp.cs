using UnityEngine;
using System.Collections;

public class HubStartUp : MonoBehaviour
{
	public GameObject hub1Load;
	public GameObject hub2Load;
	public GameObject hub3Load;
	public GameObject hub4Load;

	void Awake()
	{
//		GameDirector.instance.HubPhase5 ();
//		GameDirector.instance.StatueCrumbleThree ();

		switch (GameDirector.instance.GetHubPhase())
		{
		case 5:
			hub1Load.SetActive(false);
			hub2Load.SetActive(false);
			hub4Load.SetActive(false);
			break;
		case 1:
			hub2Load.SetActive(false);
			hub3Load.SetActive(false);
			hub4Load.SetActive(false);
			break;
		case 2:
			hub1Load.SetActive(false);
			hub3Load.SetActive(false);
			hub4Load.SetActive(false);
			break;
		case 3:
			hub1Load.SetActive(false);
			hub2Load.SetActive(false);
			hub4Load.SetActive(false);
			break;
		case 4:
			hub1Load.SetActive(false);
			hub2Load.SetActive(false);
			hub3Load.SetActive(false);
			break;
		}
	}

	void Start()
	{
	}
}