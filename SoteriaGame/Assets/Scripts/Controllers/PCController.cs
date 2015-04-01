using UnityEngine;
using System.Collections;

public class PCController : MonoBehaviour {

    private ButtonController button;
    StandardMovementController stdMovement;
    EncounterMovementController encMovement;

	//removing public GameObject reference will break rest of script
	ButtonController buttonController;

	// Use this for initialization
	void Start () {
        stdMovement = gameObject.GetComponent<StandardMovementController>();
        encMovement = gameObject.GetComponent<EncounterMovementController>();
        
		button = GameObject.Find ("Button").GetComponent<ButtonController> ();

		EnableStandardMovement();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void EnableEncounterMovement()
    {
        stdMovement.enabled = false;
        encMovement.enabled = true;

		button.Pulsing = true;

    }

    public void EnableStandardMovement()
    {
        stdMovement.enabled = true;
        encMovement.enabled = false;

		button.Pulsing = false;
    }
}