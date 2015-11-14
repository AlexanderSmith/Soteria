using UnityEngine;
using System.Collections;

public class MusicDistrictCards : MonoBehaviour
{
	public Transform whistle;
	public Transform handDoll;
	public Transform eggShells;

	public GameObject whistleCard;
	public GameObject handDollCard;
	public GameObject eggShellsCard;

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			string card = GameDirector.instance.GetMusicDistrictCard();
			if (card != null)
			{
				switch (card)
				{
				case "Whistle":
					this.NoWhistle();
					break;
				case "HandDoll":
					this.NoDoll();
					break;
				case "EggShells":
					this.NoEgg();
					break;
				};
			}
		}
		else
		{
			GameObject _whistle = Instantiate(whistleCard, whistle.position, whistle.rotation) as GameObject;
			GameObject _doll = Instantiate(handDollCard, handDoll.position, handDoll.rotation) as GameObject;
			GameObject _egg = Instantiate(eggShellsCard, eggShells.position, eggShells.rotation) as GameObject;
		}
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}

	void NoWhistle()
	{
		GameObject _doll = Instantiate(handDollCard, handDoll.position, handDoll.rotation) as GameObject;
		GameObject _egg = Instantiate(eggShellsCard, eggShells.position, eggShells.rotation) as GameObject;
	}

	void NoDoll()
	{
		GameObject _whistle = Instantiate(whistleCard, whistle.position, whistle.rotation) as GameObject;
		GameObject _egg = Instantiate(eggShellsCard, eggShells.position, eggShells.rotation) as GameObject;
	}

	void NoEgg()
	{
		GameObject _whistle = Instantiate(whistleCard, whistle.position, whistle.rotation) as GameObject;
		GameObject _doll = Instantiate(handDollCard, handDoll.position, handDoll.rotation) as GameObject;
	}
}
