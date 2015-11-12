using UnityEngine;
using System.Collections;

public class TailorInteraction : MonoBehaviour
{
	private string _musicDistrictCard;
	private string _theaterDistrictCard;
	private string _observatoryDistrictCard;

	private bool _cardsCorrect;

	void Start()
	{
		this._musicDistrictCard = "CardOfEggshells";
		this._theaterDistrictCard = "CardOfChameleon";
		this._observatoryDistrictCard = "CardOfStarChart";
		this._cardsCorrect = false;
	}

	public void CheckCards()
	{
		this._cardsCorrect = GameDirector.instance.CheckCards(this._musicDistrictCard, this._theaterDistrictCard, this._observatoryDistrictCard);
	}
}
