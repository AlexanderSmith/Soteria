using UnityEngine;
using System.Collections;

public enum GameStates
{
	Normal,
	Hidden,
	Encounter,
	Pause
}

public class StateManager : MonoBehaviour {

    private GameStates gameState;
	// Use this for initialization

	void Awake()
	{
		this.enabled = false;
	}
	
	// Update is called once per frame
	public void Update () {}

    public void Initialize() {}

	public void ChangeGameState(GameStates inState)
	{
		switch(inState)
		{
		case GameStates.Normal:
				if (gameState != inState) SwitchToNormal();
			break;
		case GameStates.Encounter:
				if (gameState != inState) SwitchToEncounter();
			break;
		case GameStates.Pause:
				if (gameState != inState) SwitchToPause();
			break;
		}
	}

	public GameStates GameState()
	{
		return gameState;
	}

	private void SwitchToEncounter()
	{

		gameState = GameStates.Encounter;
	}

	private void SwitchToNormal()
	{

		gameState = GameStates.Normal;
	}

	private void SwitchToPause()
	{

		gameState = GameStates.Pause;
	}
}
