using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	private int activeLevel;

    public int GetActiveLeve()
    {
        return activeLevel;
    }

    // Use this for initialization
    private void Awake()
    {
		LoadCurrentLevelData (Application.loadedLevel);
    }

	// Use this for initialization
	void Start ()
	{	
	}
	
	// Update is called once per frame
	void Update ()
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
