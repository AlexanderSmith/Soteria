using UnityEngine;
using System.Collections;

public class GetSuit : MonoBehaviour
{
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (GameDirector.instance.GetPlayer().GetPlayerState() == PlayerState.Dialogue && GameDirector.instance.isDialogueActive())
			{
				Destroy(this.GetComponent<SphereCollider>());
				GameDirector.instance.SuitWorn();
			}
		}
	}
}