using UnityEngine;
using System.Collections;

public class GetLantern : Reaction
{
	GameObject dreams;
	public Sprite SplashScreen;

	void Start()
	{
		dreams = GameObject.Find("DreamsDirectionDialogue");
	}

	public override void execute()
	{
		GameDirector.instance.LanternTrue();
		dreams.SetActive(true);
		GameDirector.instance.StartItemInteraction(SplashScreen);
		this.gameObject.transform.parent.gameObject.SetActive(false);
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
	}
}