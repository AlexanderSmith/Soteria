using UnityEngine;
using System.Collections;

public class EncounterManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(KickOffEncounter());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize()
	{
		
	}

    void StopEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StopEncounterMode();
    }

    void StartEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StartEncounterMode();
    }

    IEnumerator KickOffEncounter()
    {
        StartEncounter();
        return null;
    }
}
