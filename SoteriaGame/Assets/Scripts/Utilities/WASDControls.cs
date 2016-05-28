using UnityEngine;
using System.Collections;

public class WASDControls : MonoBehaviour
{
	public void ShowControls()
	{
		GameDirector.instance.WASDSetup();
		GameDirector.instance.EndTriggerState();
	}
}