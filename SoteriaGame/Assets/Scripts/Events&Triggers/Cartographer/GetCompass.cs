using UnityEngine;
using System.Collections;

public class GetCompass : Reaction
{
	GameObject ptLights;
	public Sprite SplashScreen;

	void Start()
	{
		ptLights = GameObject.Find ("SoteriaPowerSystem_Lights");
	}

	public override void execute ()
	{
		GameDirector.instance.CompassTrue();
		ptLights.SetActive(true);
		GameDirector.instance.StartItemInteraction(SplashScreen);
		this.gameObject.transform.parent.gameObject.SetActive(false);
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
	}
}
