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

    IEnumerator KickOffEncounter()
    {
        StartEncounter();
        return null;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(KickOffEncounter());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {

    }

    public void StopEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StopEncounterMode();
    }

    public void StartEncounter()
    {
        this.gameObject.GetComponent<GameDirector>().StartEncounterMode();
    }
}
