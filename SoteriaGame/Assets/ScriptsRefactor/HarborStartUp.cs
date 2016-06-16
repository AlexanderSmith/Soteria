using UnityEngine;
using System.Collections;

public class HarborStartUp : MonoBehaviour
{
	public GameObject harborLoad;
	public GameObject harborRespawnLoad;

	void Awake()
	{
		if (!GameDirector.instance.GetToken() && GameDirector.instance.GetGamePhase() != 4)
		{
			harborRespawnLoad.SetActive(false);
		}
		else
		{
			harborLoad.SetActive(false);
		}
	}
}