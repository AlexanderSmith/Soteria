using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour{

    public Transform player;
	Transform safeArea;
	GameObject agent;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
	}


    public void EnableSafetyLight()
    {
        this.light.enabled = true;
        this.collider.enabled = true;
    }

    public void DisableSafetyLight()
    {
        this.light.enabled = false;
        this.collider.enabled = false;
    }

    public void OnPress()
    {
		agent = GameObject.FindWithTag ("NavMesh Agent");
		agent.SendMessage ("EnableAgent");
        this.GetComponent<SafetyLightController>().EnableSafetyLight();
        player.GetComponent<PCController>().EnableStandardMovement();
        player.GetComponent<EncounterMovementController>().CheckEscape();
    }
}
