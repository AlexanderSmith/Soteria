using UnityEngine;
using System.Collections;

public class EncounterManager : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		enabled = false;
	}

	void Start()
	{
		//Commented this out as it was giving an error!
		//StartCoroutine(KickOffEncounter()); 
		StartEncounter();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		Start();
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
