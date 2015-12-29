using UnityEngine;
using System.Collections;

public class GetCompass : Reaction
{
	GameObject ptLights;

	void Start()
	{
		ptLights = GameObject.Find ("SoteriaPowerSystem_Lights");
	}

	public override void execute ()
	{
		GameDirector.instance.CompassTrue();
		ptLights.SetActive(true);
	}
}
