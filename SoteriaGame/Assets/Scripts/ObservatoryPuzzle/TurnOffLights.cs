using UnityEngine;
using System.Collections;

public class TurnOffLights : MonoBehaviour
{
	void Start()
	{
		this.GetComponentInChildren<Light>().enabled = false;
	}
}
