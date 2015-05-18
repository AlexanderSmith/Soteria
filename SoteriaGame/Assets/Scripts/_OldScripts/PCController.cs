using UnityEngine;
using System.Collections;

public class PCController : MonoBehaviour
{
    EncounterMovementController encMovement;

	// Use this for initialization
	void Start ()
	{
        encMovement = gameObject.GetComponent<EncounterMovementController>();
		EnableStandardMovement();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    public void EnableEncounterMovement()
    {
        encMovement.enabled = true;
    }

    public void EnableStandardMovement()
    {
        encMovement.enabled = false;
    }
}