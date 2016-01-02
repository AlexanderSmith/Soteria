using UnityEngine;
using System.Collections;

public class TailorInteraction : MonoBehaviour
{
	private string _musicDistrictCard;
	private string _theaterDistrictCard;
	private string _observatoryDistrictCard;

	private bool _cardsCorrect;

	private GameObject _tailorFirstDialogue;
	private GameObject _tailorCorrectCards;
	private GameObject _tailorIncorrectCards;
	public Transform tailorPrefabLocation;

	GameObject tailorLight;

	void Awake()
	{
		this._musicDistrictCard = "EggShells";
		this._theaterDistrictCard = "Chameleon";
		this._observatoryDistrictCard = "StarChart";
		this._tailorFirstDialogue = GameObject.Find("TailorFirstInteraction");
		this._tailorCorrectCards = GameObject.Find("TailorCorrectCards");
		this._tailorIncorrectCards = GameObject.Find("TailorIncorrectCards");
		this._tailorFirstDialogue.SetActive(false);
		this._tailorCorrectCards.SetActive(false);
		this._tailorIncorrectCards.SetActive(false);
		this.tailorLight = GameObject.Find("TailorLight");
		tailorLight.GetComponent<Light>().enabled = false;
	}

	void Start()
	{
//		GameDirector.instance.TailorSpokenTo();
//		GameDirector.instance.MusicCardCollected (this._musicDistrictCard);
//		GameDirector.instance.TheaterCardCollected (this._theaterDistrictCard);
//		GameDirector.instance.ObservatoryCardCollected (this._observatoryDistrictCard);
		if (!GameDirector.instance.GetFirstTailorInteraction())
		{
			this._tailorFirstDialogue.SetActive(true);
			GameDirector.instance.TailorSpokenTo();
		}
		else
		{
			if (GameDirector.instance.CardsHeld() == 3)
			{
				this._cardsCorrect = GameDirector.instance.CheckCards(this._musicDistrictCard, this._theaterDistrictCard, this._observatoryDistrictCard);
				tailorLight.GetComponent<Light>().enabled = true;
				if (this._cardsCorrect)
				{
					this._tailorCorrectCards.SetActive(true);
				}
				else
				{
					this._tailorIncorrectCards.SetActive(true);
				}
			}
		}
	}
}
