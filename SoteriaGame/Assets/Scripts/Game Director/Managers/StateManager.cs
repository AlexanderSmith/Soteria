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

	void Awake()
	{
		this.enabled = false;
	}
	
	// Update is called once per frame
	public void Update () {}

    public void Initialize() {}
}
