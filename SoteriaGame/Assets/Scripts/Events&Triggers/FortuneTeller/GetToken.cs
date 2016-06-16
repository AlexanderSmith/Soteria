using UnityEngine;
using System.Collections;

public class GetToken : Reaction
{
	public Sprite SplashScreen;
	private string itemText;
	void Start()
	{
		itemText = "-Teleports you to town center\n-Left mouse-click to activate\n-Unlimited use";
	}
	
	public override void execute ()
	{
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
		GameDirector.instance.TokenTrue();
		GameDirector.instance.StartItemInteraction(SplashScreen, itemText);
		this.gameObject.transform.parent.parent.gameObject.SetActive(false);
	}
}