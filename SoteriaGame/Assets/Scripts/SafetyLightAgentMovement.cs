using UnityEngine;
using System.Collections;

public class SafetyLightAgentMovement : MonoBehaviour
{
	NavMeshAgent agent;
	Transform safeArea;
	GameObject[] listOfSafeAreas;
	GameObject safetyLight;
	GameObject player;

	enum State
	{
		neutral,
		moving,
	};

	State currentState = State.neutral;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent> ();
		DisableAgent ();
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentState == State.moving)
		{
			agent.SetDestination (safeArea.position);
			if (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), safeArea.position) <= 1.0f)
			{
				safetyLight.GetComponent<SafetyLightController>().DisableSafetyLight();
				currentState = State.neutral;
				DisableAgent();
			}
		}
	}

	Transform FindNearestSafeArea(GameObject[] listOfSafeAreas)
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

		return closestSafe;
	}

	public void EnableAgent()
	{
		agent.enabled = true;
		currentState = State.moving;
		listOfSafeAreas = GameObject.FindGameObjectsWithTag ("SafeArea");
		safetyLight = GameObject.FindWithTag ("Safety Light");
		player = GameObject.FindWithTag ("Player");
		this.transform.position = player.transform.position;
		safeArea = FindNearestSafeArea (listOfSafeAreas);
	}

	void DisableAgent()
	{
		agent.enabled = false;
		currentState = State.neutral;
	}
}
