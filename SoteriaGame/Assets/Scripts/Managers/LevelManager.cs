using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{

    private int activeLevel;
	
	public int GetActiveLeve()
    {
        return activeLevel;
    }

	public void Awake()
	{
		LoadCurrentLevelData (Application.loadedLevel);
	}

	public void Start()
	{
	}

	public void update()
	{
	}

	public void Initialize()
	{

	}
	public void LoadLevel(int level)
	{
		Debug.Log ("LoadLevel - LevelManager");
		activeLevel = level;
		Application.LoadLevel (level);
		Application.LoadLevelAdditive ("DebugScene");
	}

	private void LoadCurrentLevelData (int level)
	{
		switch(level)
		{
			case 0: LoadHarborLevel(); break;
				
			case 1: LoadHubLevel();	break;
				
			//case 2: //Obs...etc break;
		}   
	}
	void OnLevelWasLoaded(int level) 
	{
		LoadCurrentLevelData (level);
	}
	void LoadHarborLevel() { }

	void LoadHubLevel(){ }
}
