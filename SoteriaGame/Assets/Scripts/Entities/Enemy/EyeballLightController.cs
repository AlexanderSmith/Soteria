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
		GameDirector.instance.ChangeVolume(AudioID.Whispers, 1.0f);
	}

	public void NormalColor()
	{
		this.GetComponent<Light>().color = Color.white;
		GameDirector.instance.ChangeVolume(AudioID.Whispers, 0.0f);
	}
}
