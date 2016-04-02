using UnityEngine;
using System.Collections;

public class GetCompass : Reaction
{
	public GameObject ptLights;
	public Sprite SplashScreen;
	private string itemText;

	void Start()
	{
		itemText = "-Keeps you oriented and on track\n-Compass Icon marks next goal\n-Arrow points in direction of goal\n-If no icon or arrow displayed, complete objective as you see fit";
	}

	public override void execute ()
	{
		GameDirector.instance.CompassTrue();
		ptLights.SetActive(true);
		GameDirector.instance.StartItemInteraction(SplashScreen, itemText);
		this.gameObject.transform.parent.gameObject.SetActive(false);
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
	}
}
