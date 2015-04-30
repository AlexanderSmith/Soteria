using UnityEngine;
using System.Collections;

public class NavMeshAgentMovement : MonoBehaviour
{
	NavMeshAgent agent;
	Transform safeArea;
	GameObject[] listOfSafeAreas;
	GameObject safetyLight;
	GameObject player;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		//agent.enabled = true;
		//player = GameObject.Find ("Player");
		//agent.transform.position = player.transform.position;
		listOfSafeAreas = GameObject.FindGameObjectsWithTag ("SafeArea");
		safeArea = FindNearestSafeArea (listOfSafeAreas);
		safetyLight = GameObject.Find ("Safety Light");
	}
	
	// Update is called once per frame
	void Update ()
	{
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (safeArea.position);
		if (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), safeArea.position) <= 1.0f)
		{
			safetyLight.SendMessage("DisableSafetyLight");
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
}
