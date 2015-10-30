using UnityEngine;
using System.Collections;

public class ObservatoryPass1Done : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ObservatoryPass1Done();
		}
	}
}
