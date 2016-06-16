using UnityEngine;
using System.Collections;

public class SewerVisited : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameDirector.instance.VisitedSewer();
		}
	}
}
