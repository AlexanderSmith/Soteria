using UnityEngine;
using System.Collections;

public class FirstTile : MonoBehaviour
{
	private ObservatoryPuzzleController _controller;

	void Start()
	{
		this._controller = GameObject.Find("ObsPuzzleController").GetComponent<ObservatoryPuzzleController>();
	}

	void OnTriggerEnter(Collider player)
	{
		if (GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			if (player.gameObject.tag == "Player" && !this._controller.GetOffPath())
			{
				this._controller.EnableTileCtrLight();
			}
		}
	}
}