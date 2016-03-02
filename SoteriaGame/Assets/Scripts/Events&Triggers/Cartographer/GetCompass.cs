using UnityEngine;
using System.Collections;

public class GetCompass : Reaction
{
	public GameObject ptLights;
	public Sprite SplashScreen;

	void Start()
	{
//		ptLights = GameObject.Find ("SoteriaPowerSystem_Lights");
//		ptLights.SetActive(false);
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
