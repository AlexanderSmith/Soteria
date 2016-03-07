using UnityEngine;
using System.Collections;

public class GetLantern : Reaction
{
	GameObject dreams;
	private GameObject _hubToMusic;
	public Sprite SplashScreen;
	
	void Start()
	{
		//dreams = GameObject.Find("DreamsDirectionDialogue");
//		_hubToMusic = GameObject.Find("HubToMusic");
//		_hubToMusic.SetActive(false);
	}

	public override void execute()
	{
		GameDirector.instance.LanternTrue();
		dreams.SetActive(true);
		_hubToMusic.SetActive(true);
		GameDirector.instance.ChangeObjective (_hubToMusic);
		GameDirector.instance.StartItemInteraction(SplashScreen);
		this.gameObject.transform.parent.gameObject.SetActive(false);
		GameDirector.instance.GetPlayer().PlayerActionItemPickup();
	}

	public void SetHubToMusic(GameObject inGobj)
	{
		_hubToMusic = inGobj;
		if (!GameDirector.instance.GetLantern())
		{
			_hubToMusic.SetActive (false);
		}
	}

	public void SetDreams(GameObject inDreams)
	{
		dreams = inDreams;
	}
}