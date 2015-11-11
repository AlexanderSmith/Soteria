using UnityEngine;
using System.Collections;

public class GetToken: MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameDirector.instance.TokenTrue();
		}
	}
}
