using UnityEngine;
using System.Collections;

public class HarborToHub : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("HUBProxy");
		}
	}
}