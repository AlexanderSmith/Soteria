using UnityEngine;
using System.Collections;

public class MusicDistrictCards : MonoBehaviour
{
	public Transform whistle;
	public Transform handDoll;
	public Transform eggShells;

	private GameObject _whistleCard;
	private GameObject _handDollCard;
	private GameObject _eggShellsCard;

	//void OnTriggerEnter(Collider player)
	void Awake()
	{
		this._whistleCard = GameObject.Find("Whistle");
		this._whistleCard.transform.position = this.whistle.position;
		this._handDollCard = GameObject.Find("HandDoll");
		this._handDollCard.transform.position = this.handDoll.position;
		this._eggShellsCard = GameObject.Find("EggShells");
		this._eggShellsCard.transform.position = eggShells.position;
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
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}

	void NoWhistle()
	{
		this._whistleCard.SetActive(false);
	}

	void NoDoll()
	{
		this._handDollCard.SetActive(false);
	}

	void NoEgg()
	{
		this._eggShellsCard.SetActive(false);
	}
}
