using UnityEngine;
using System.Collections;

public class TailorInteraction : MonoBehaviour
{
	private string _musicDistrictCard;
	private string _theaterDistrictCard;
	private string _observatoryDistrictCard;

	private bool _cardsCorrect;

	public GameObject tailorFirstDialogue;
	public GameObject tailorCorrectCards;
	public GameObject tailorIncorrectCards;
	public Transform tailorPrefabLocation;

	void Start()
	{
		this._musicDistrictCard = "EggShells";
		this._theaterDistrictCard = "Chameleon";
		this._observatoryDistrictCard = "StarChart";
//		GameDirector.instance.TailorSpokenTo();
//		GameDirector.instance.MusicCardCollected (this._musicDistrictCard);
//		GameDirector.instance.TheaterCardCollected (this._theaterDistrictCard);
//		GameDirector.instance.ObservatoryCardCollected (this._observatoryDistrictCard);
		this._cardsCorrect = GameDirector.instance.CheckCards(this._musicDistrictCard, this._theaterDistrictCard, this._observatoryDistrictCard);
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.GetFirstTailorInteraction())
			{
				GameObject tailorPrefab = Instantiate(tailorFirstDialogue, tailorPrefabLocation.position, tailorPrefabLocation.rotation) as GameObject;
			}
			else
			{
				if (this._cardsCorrect)
				{
					GameObject tailorPrefab = Instantiate(tailorCorrectCards, tailorPrefabLocation.position, tailorPrefabLocation.rotation) as GameObject;
				}
				else
				{
					GameObject tailorPrefab = Instantiate(tailorIncorrectCards, tailorPrefabLocation.position, tailorPrefabLocation.rotation) as GameObject;
				}
			}
		}
	}
}
