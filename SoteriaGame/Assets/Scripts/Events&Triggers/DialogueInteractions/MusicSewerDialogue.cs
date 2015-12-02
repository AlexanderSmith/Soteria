using UnityEngine;
using System.Collections;

public class MusicSewerDialogue : MonoBehaviour
{
	private GameObject _oMalleyPrefab;
	public Transform oMalleySpawnLoc;

	void Awake()
	{
		_oMalleyPrefab = GameObject.Find ("O'MalleyMusicSewer");
		_oMalleyPrefab.SetActive(false);
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaTriesGateMUSICp1");
			GameDirector.instance.StartDialogue();
			this.gameObject.GetComponentInChildren<InspectMusicSewer>().TurnOffInspect();
			oMalleySpawnLoc.LookAt(GameDirector.instance.GetPlayer().transform.position);
			_oMalleyPrefab.transform.position = oMalleySpawnLoc.position;
			_oMalleyPrefab.transform.rotation = oMalleySpawnLoc.rotation;
		}
	}
	
	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				_oMalleyPrefab.SetActive(true);
				GameDirector.instance.PlayAudioClip(AudioID.OMalleyMeow);
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}