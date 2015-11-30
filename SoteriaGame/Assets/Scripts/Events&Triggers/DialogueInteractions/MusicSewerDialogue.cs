using UnityEngine;
using System.Collections;

public class MusicSewerDialogue : MonoBehaviour
{
	public GameObject oMalleyPrefab;
	public Transform oMalleySpawnLoc;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaTriesGateMUSICp1");
			GameDirector.instance.StartDialogue();
			this.gameObject.GetComponentInChildren<InspectMusicSewer>().TurnOffInspect();
		}
	}
	
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameObject oMalley = Instantiate(oMalleyPrefab, oMalleySpawnLoc.position, oMalleySpawnLoc.rotation) as GameObject;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}