using UnityEngine;
using System.Collections;


/// <summary>
/// Encounter manager.
/// 
/// Possible Additions: 
/// Set the Encounter States -> what's the state of the player: winning, losing, escaping ecc.
/// 
/// </summary>


public class EncounterManager : MonoBehaviour {

	private bool _startEnc;
	private bool _encActive;

	// Use this for initialization
	void Awake ()
	{
		this.enabled = false;

		this._encActive = false;
		this._startEnc = false;
	}

	// Update is called once per frame
	public void Update () 
	{
		if (this._startEnc && !this._encActive)
			this.UpdateEncounter(true);
		else if (this._encActive && !this._startEnc)
			this.UpdateEncounter(false);
	}

	public void Initialize()
	{
		
	}

    private void UpdateEncounter(bool inStatus)
	{
		this._encActive = inStatus;
		GameDirector.instance.UpdateEncounterState(this._encActive);
    }

	public void ActivateEncounter()
	{
		this._startEnc = true;
	}

	public void DeActivateEncounter()
	{
		this._startEnc = false;
	}
}
