using UnityEngine;
using System.Collections;

public class GetToken : Reaction
{
	public Sprite SplashScreen;
	
	void Start()
	{

	}

	public override void execute ()
	{
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
		GameDirector.instance.TokenTrue();
		GameDirector.instance.StartItemInteraction(SplashScreen);
		this.gameObject.transform.parent.parent.gameObject.SetActive(false);
	}
}
