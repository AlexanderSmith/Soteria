using UnityEngine;
using System.Collections;

public class GetLantern : Reaction
{
	GameObject dreams;
	private GameObject _hubToMusic;
	public Sprite SplashScreen;
	
	void Start()
	{
		dreams = GameObject.Find("DreamsDirectionDialogue");
		_hubToMusic = GameObject.Find("HubToMusic");
	}

	public override void execute()
	{
		GameDirector.instance.LanternTrue();
		dreams.SetActive(true);
		_hubToMusic.SetActive(true);
		GameDirector.instance.StartItemInteraction(SplashScreen);
		this.gameObject.transform.parent.gameObject.SetActive(false);
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
	}
}