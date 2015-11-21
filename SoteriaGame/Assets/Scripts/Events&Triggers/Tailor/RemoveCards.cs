using UnityEngine;
using System.Collections;

public class RemoveCards : MonoBehaviour
{
	private string _musicDistrictCard = "EggShells";
	private string _theaterDistrictCard = "Chameleon";
	private string _observatoryDistrictCard = "StarChart";

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (GameDirector.instance.GetPlayer().GetPlayerState() == PlayerState.Dialogue && GameDirector.instance.isDialogueActive())
			{
				GameDirector.instance.RemoveCards(_musicDistrictCard, _theaterDistrictCard, _observatoryDistrictCard);
			}
		}
	}
}
