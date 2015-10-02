using UnityEngine;
using System.Collections;

public class GetCompass : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		GameDirector.instance.CompassTrue();
	}
}
