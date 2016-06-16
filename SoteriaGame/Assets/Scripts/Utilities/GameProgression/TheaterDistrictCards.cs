using UnityEngine;
using System.Collections;

public class TheaterDistrictCards : MonoBehaviour
{
	public Transform pearls;
	public Transform hat;
	public Transform chameleon;
	
	private GameObject _pearlsCard;
	private GameObject _hatCard;
	private GameObject _chameleonCard;

	public GameObject portToPuzzle;

	void Start()
	{
		portToPuzzle.SetActive(false);
	}
	
	void Awake()/*OnTriggerEnter(Collider player)*/
	{
		this._pearlsCard = GameObject.Find("StringOfPearls");
		this._pearlsCard.transform.position = this.pearls.position;
		this._hatCard = GameObject.Find("PartyHat");
		this._hatCard.transform.position = this.hat.position;
		this._chameleonCard = GameObject.Find("Chameleon");
		this._chameleonCard.transform.position = this.chameleon.position;
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
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	void NoPearls()
	{
		this._pearlsCard.SetActive(false);
	}
	
	void NoHat()
	{
		this._hatCard.SetActive(false);
	}
	
	void NoChameleon()
	{
		this._chameleonCard.SetActive(false);
	}
}
