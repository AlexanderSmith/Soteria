using UnityEngine;
using System.Collections;

public class SceneHub3 : Scene 
{	
	private GameObject _oMalleySuitOff;
	private GameObject _oMalleyStatue;
	
	public GameObject hubToMusic;
	public GameObject hubToTheater;
	public GameObject hubToObservatory;
	public GameObject oMalley;
	public GameObject soteriaStatue;

	const SceneFlags FailedTheater  	= SceneFlags.HEX_10000000;

	WorldFlags worldFlagState 	= WorldFlags.EMPTY_FLAG;

	void Awake()
	{
		this.sceneName = SceneName.HUB_3;
		this._oMalleySuitOff = GameObject.Find("O'MalleySuitOff");
		this._oMalleyStatue = GameObject.Find("O'MalleySoteriaStatue");
		this._oMalleyStatue.SetActive(false);
	}
	
	void Start()
	{
		worldFlagState = ProgressionManager.instance.Flags_World;

		if (FlagTools.World_CheckFlag(WorldFlags.ITEM_SUIT, worldFlagState)) 
		{
			if(FlagTools.World_CheckFlag(WorldFlags.MUSIC_WITH_SUIT, worldFlagState)) 
			{
				GameDirector.instance.ChangeObjective(hubToMusic);
				this._oMalleySuitOff.SetActive(false);
				return;
			}
			else if(FlagTools.World_CheckFlag(WorldFlags.THEATER_WITH_SUIT, worldFlagState))
			{
				GameDirector.instance.ChangeObjective(hubToTheater);
				this._oMalleySuitOff.SetActive(false);
				return;
			}
			else if(FlagTools.World_CheckFlag(WorldFlags.OBS_WITH_SUIT, worldFlagState))
			{
				GameDirector.instance.ChangeObjective(hubToObservatory);
				this._oMalleySuitOff.SetActive(false);
				return;
			}
		} 
		else 
		{
			this._oMalleySuitOff.SetActive(false);
			if (FlagTools.World_CheckFlag(WorldFlags.TUTORIAL_COMPLETE, worldFlagState))
			{
				if(FlagTools.World_CheckFlag(WorldFlags.ITEM_COMPASS, worldFlagState))
				{
					if(FlagTools.World_CheckFlag(WorldFlags.MUSIC_DEFEATED, worldFlagState))
					{
						GameDirector.instance.ChangeObjective(hubToMusic);
						return;
					}
					else if(FlagTools.World_CheckFlag(WorldFlags.THEATER_DEFEATED, worldFlagState))
					{
						GameDirector.instance.ChangeObjective(hubToTheater);
						return;
					}
					else if(FlagTools.World_CheckFlag(WorldFlags.OBS_DEFEATED, worldFlagState))
					{
						GameDirector.instance.ChangeObjective(hubToObservatory);
						return;
					}
				}

				GameDirector.instance.ChangeObjective(soteriaStatue);
			}
			
			else if (FlagTools.World_CheckFlag(WorldFlags.MUSIC_DEFEATED, worldFlagState) && FlagTools.World_CheckFlag(WorldFlags.THEATER_DEFEATED, worldFlagState) && FlagTools.World_CheckFlag(WorldFlags.OBS_DEFEATED, worldFlagState))
			{
				this._oMalleyStatue.SetActive(true);
			}
		}
		
	}
}
