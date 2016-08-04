using UnityEngine;
using System.Collections;

public class TriggerTailorLight : MonoBehaviour
{
	public GameObject tailorLight;

	public void TurnOn()
	{
		if (tailorLight != null)
		{
			//this.tailorLight = GameObject.Find("TailorLight");
			this.tailorLight.GetComponent<Light>().enabled = true;
		}
		GameDirector.instance.EndTriggerState();
	}
}
