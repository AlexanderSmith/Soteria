using UnityEngine;
using System.Collections;

public class EyeballLightController : MonoBehaviour
{
	public NavMeshAgent _agent;

//	void Start()
//	{
//		_agent = GetComponentInParent<NavMeshAgent>();
//	}

	void Update()
	{
		transform.LookAt (_agent.transform.position);
	}
}
