using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    protected static LevelManager _instance;
    private string activeLevel;

    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<LevelManager>();
            return (LevelManager)(_instance);
        }
    }

    public string GetActiveLeve()
    {
        return activeLevel;
    }

    public void SetActiveLevel(string level)
    {
        activeLevel = level;
        Application.LoadLevel(level);
    }

    // Use this for initialization
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this); //Keep the instance going between scenes
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
