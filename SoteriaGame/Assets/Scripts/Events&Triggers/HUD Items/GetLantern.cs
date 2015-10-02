using UnityEngine;
using System.Collections;

public class GetLantern : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		GameDirector.instance.LanternTrue();
	}
}
