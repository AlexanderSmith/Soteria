using UnityEngine;
using System.Collections;

public class TurnOffLights : MonoBehaviour
{
	void Start()
	{
		if (GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			this.GetComponentInChildren<Light>().enabled = false;
		}
	}
}
