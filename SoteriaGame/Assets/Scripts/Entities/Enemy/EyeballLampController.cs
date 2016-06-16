using UnityEngine;
using System.Collections;

public class EyeballLampController : MonoBehaviour
{
	public NavMeshAgent agent;

	// Use this for initialization
	void Start ()
	{
		Vector3 target = new Vector3 (agent.transform.position.x, this.transform.position.y, agent.transform.position.z);
		this.transform.LookAt(target);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 target = new Vector3 (agent.transform.position.x, this.transform.position.y, agent.transform.position.z);
		this.transform.LookAt(target);
	}
}
