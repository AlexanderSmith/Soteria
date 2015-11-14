using UnityEngine;
using System.Collections;

public class TheaterDistrictCards : MonoBehaviour
{
	public Transform pearls;
	public Transform hat;
	public Transform chameleon;
	
	public GameObject pearlsCard;
	public GameObject hatCard;
	public GameObject chameleonCard;
	
	void OnTriggerEnter(Collider player)
	{
		string card = GameDirector.instance.GetTheaterDistrictCard();
		if (card != null)
		{
			switch (card)
			{
			case "StringOfPearls":
				this.NoPearls();
				break;
			case "PartyHat":
				this.NoHat();
				break;
			case "Chameleon":
				this.NoChameleon();
				break;
			};
		}
		else
		{
			GameObject _pearls = Instantiate(pearlsCard, pearls.position, pearls.rotation) as GameObject;
			GameObject _hat = Instantiate(hatCard, hat.position, hat.rotation) as GameObject;
			GameObject _chameleon = Instantiate(chameleonCard, chameleon.position, chameleon.rotation) as GameObject;
		}
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	void NoPearls()
	{
		GameObject _hat = Instantiate(hatCard, hat.position, hat.rotation) as GameObject;
		GameObject _chameleon = Instantiate(chameleonCard, chameleon.position, chameleon.rotation) as GameObject;
	}
	
	void NoHat()
	{
		GameObject _pearls = Instantiate(pearlsCard, pearls.position, pearls.rotation) as GameObject;
		GameObject _chameleon = Instantiate(chameleonCard, chameleon.position, chameleon.rotation) as GameObject;
	}
	
	void NoChameleon()
	{
		GameObject _pearls = Instantiate(pearlsCard, pearls.position, pearls.rotation) as GameObject;
		GameObject _hat = Instantiate(hatCard, hat.position, hat.rotation) as GameObject;
	}
}
