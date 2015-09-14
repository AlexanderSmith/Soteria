using UnityEngine;
using System.Collections;

public class EyeballLightController : MonoBehaviour
{
	public NavMeshAgent _agent;

	void Start()
	{
		this.GetComponent<Light>().color = Color.white;
	}

	void Update()
	{
		transform.LookAt (_agent.transform.position);
	}

	public void AlarmColor()
	{
		this.GetComponent<Light>().color = Color.red;
	}

	public void NormalColor()
	{
		this.GetComponent<Light>().color = Color.white;
	}
}
