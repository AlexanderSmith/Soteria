using UnityEngine;
using System.Collections;

public class GetSuit : Reaction
{

	public Sprite SplashScreen;
	public string itemText;

	public override void execute()
	{
		GameDirector.instance.SuitWorn();
		GameDirector.instance.ChangeObjective(GameObject.Find("HubToMusic"));
		GameDirector.instance.HubPhase5();
		GameDirector.instance.AddGamePhase();

		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
		GameDirector.instance.StartItemInteraction(SplashScreen);
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}
}