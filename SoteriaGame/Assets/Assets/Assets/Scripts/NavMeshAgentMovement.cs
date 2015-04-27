using UnityEngine;
using System.Collections;

public class NavMeshAgentMovement : MonoBehaviour
{
	NavMeshAgent agent;
	public Transform player;
	Transform safeArea;
	GameObject[] listOfSafeAreas;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		listOfSafeAreas = GameObject.FindGameObjectsWithTag ("SafeArea");
		safeArea = FindNearestSafeArea (listOfSafeAreas);
	}
	
	// Update is called once per frame
	void Update ()
	{
			
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
