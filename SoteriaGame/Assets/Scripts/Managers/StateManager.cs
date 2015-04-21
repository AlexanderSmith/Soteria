using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public enum GameStates
    {
        Normal,
        Encounter,
        Pause
    }

    public GameStates gameState;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize()
    {

    }
}
