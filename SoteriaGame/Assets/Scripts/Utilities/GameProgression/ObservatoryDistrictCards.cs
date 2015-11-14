using UnityEngine;
using System.Collections;

public class ObservatoryDistrictCards : MonoBehaviour
{
	public Transform backpack;
	public Transform trainTic;
	public Transform starChart;
	
	public GameObject backpackCard;
	public GameObject trainCard;
	public GameObject starCard;
	
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
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
		}
		else
		{
			GameObject _backpack = Instantiate(backpackCard, backpack.position, backpack.rotation) as GameObject;
			GameObject _trainTic = Instantiate(trainCard, trainTic.position, trainTic.rotation) as GameObject;
			GameObject _starChart = Instantiate(starCard, starChart.position, starChart.rotation) as GameObject;
		}
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	void NoBackpack()
	{
		GameObject _trainTic = Instantiate(trainCard, trainTic.position, trainTic.rotation) as GameObject;
		GameObject _starChart = Instantiate(starCard, starChart.position, starChart.rotation) as GameObject;
	}
	
	void NoTrainTic()
	{
		GameObject _backpack = Instantiate(backpackCard, backpack.position, backpack.rotation) as GameObject;
		GameObject _starChart = Instantiate(starCard, starChart.position, starChart.rotation) as GameObject;
	}
	
	void NoStarChart()
	{
		GameObject _backpack = Instantiate(backpackCard, backpack.position, backpack.rotation) as GameObject;
		GameObject _trainTic = Instantiate(trainCard, trainTic.position, trainTic.rotation) as GameObject;
	}
}
