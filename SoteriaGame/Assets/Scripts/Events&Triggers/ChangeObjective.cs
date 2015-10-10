using UnityEngine;
using System.Collections;

public class ChangeObjective : MonoBehaviour
{
	public GameObject nextObjective;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.ChangeObjective(nextObjective);
		}
	}
}
