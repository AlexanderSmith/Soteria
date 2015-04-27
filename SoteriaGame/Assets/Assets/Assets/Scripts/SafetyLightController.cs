using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour{

    public Transform player;
	Transform safeArea;
	GameObject[] listOfSafeAreas;

	public enum State
	{
		Finding,
		Moving
	};
	State currentState;

	float lightSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		currentState = State.Finding;
		//listOfSafeAreas = GameObject.FindGameObjectsWithTag ("SafeArea");
        //safeArea = FindNextClosest(listOfSafeAreas);
	}
	
	// Update is called once per frame
	void Update () 
	{
//			this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(safeArea.position.x, this.transform.position.y, safeArea.position.z), 
//		                                              Time.deltaTime * lightSpeed);
		if (Vector3.Distance(this.transform.position, player.transform.position) >= 15.0f)
		{
        	Vector3 normal = player.forward;
                //Debug.Log ("Player pos: " + player.position);
                //Debug.Log ("Player normal: " + player.forward);
            normal.Normalize();
                //Debug.Log ("Normalized: " + normal);
                //this.transform.position = new Vector3(5 * normal.x + player.position.x, this.transform.position.y, 5 * normal.z + player.position.z);
                //if (player.GetComponent<EncounterMovementController>().GetCurrentState() == EncounterMovementController.EncounterState.Normal)
                //{
                //    DisableSafetyLight();
                //}
		}
	}

	Transform FindNextClosest(GameObject[] listOfSafeAreas)
	{
		Transform closestSafe = null;
		float closest = Mathf.Infinity;
		Vector3 currentPos = this.transform.position;
		Vector3 toTarget;
		float distSqredToTarget;

		foreach (GameObject gObj in listOfSafeAreas)
		{
			toTarget = gObj.transform.position - currentPos;
			distSqredToTarget = toTarget.sqrMagnitude;
			if (distSqredToTarget < closest)
			{
				closest = distSqredToTarget;
				closestSafe = gObj.transform;
			}
		}

		currentState = State.Moving;
		return closestSafe;
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
        this.GetComponent<SafetyLightController>().EnableSafetyLight();
        Vector3 normal = player.forward;
        //Debug.Log ("Player pos: " + player.position);
        //Debug.Log ("Player normal: " + player.forward);
        normal.Normalize();
        //Debug.Log ("Normalized: " + normal);
        this.transform.position = new Vector3(5 * normal.x + player.position.x, this.transform.position.y, 5 * normal.z + player.position.z);
        //Debug.Log ("Light pos: " + safetyLight.transform.position);
        player.GetComponent<PCController>().EnableStandardMovement();
        player.GetComponent<EncounterMovementController>().CheckEscape();
    }
}
