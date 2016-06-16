using UnityEngine;
using System.Collections;

public class GetLantern : Reaction
{
	GameObject dreams;
	private GameObject _hubToMusic;
	public Sprite SplashScreen;
	private string itemText;

	void Start()
	{
		itemText = "-Produces flash of light\n-Stuns Shadow Creature(s) within area of effect\n-Pulses when Shadow Creature(s) within range of stun\n-Left mouse-click to activate\n-Disacharges after use\n-Recharges automatically in town center";
	}

	public override void execute()
	{
		GameDirector.instance.LanternTrue();
		dreams.SetActive(true);
		_hubToMusic.SetActive(true);
		GameDirector.instance.ChangeObjective (_hubToMusic);
		GameDirector.instance.StartItemInteraction(SplashScreen , itemText);
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