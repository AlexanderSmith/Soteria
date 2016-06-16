using UnityEngine;
using System.Collections;

public class ObservatoryDistrictCards : MonoBehaviour
{
	public Transform backpack;
	public Transform trainTic;
	public Transform starChart;
	
	private GameObject _backpackCard;
	private GameObject _trainCard;
	private GameObject _starCard;

	public GameObject portToPuzzle;

	void Start()
	{
		portToPuzzle.SetActive(false);
	}
	
	void Awake()/*OnTriggerEnter(Collider player)*/
	{
		this._backpackCard = GameObject.Find("BackpackAndJournal");
		this._backpackCard.transform.position = this.backpack.position;
		this._trainCard = GameObject.Find("TrainTicket");
		this._trainCard.transform.position = this.trainTic.position;
		this._starCard = GameObject.Find("StarChart");
		this._starCard.transform.position = this.starChart.position;
		string card = GameDirector.instance.GetObservatoryDistrictCard();
		if (card != null)
		{
			switch (card)
			{
			case "BackpackAndJournal":
				this.NoBackpack();
				break;
			case "TrainTicket":
				this.NoTrainTic();
				break;
			case "StarChart":
				this.NoStarChart();
				break;
			};
		}
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	void NoBackpack()
	{
		this._backpackCard.SetActive(false);
	}
	
	void NoTrainTic()
	{
		this._trainCard.SetActive(false);
	}
	
	void NoStarChart()
	{
		this._starCard.SetActive(false);
	}
}
