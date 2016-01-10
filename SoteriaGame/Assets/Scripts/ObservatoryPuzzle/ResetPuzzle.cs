using UnityEngine;
using System.Collections;

public class ResetPuzzle : MonoBehaviour
{
	private GameObject _controller;
	private Transform _start;

	void Start()
	{
		this._controller = GameObject.Find("ObsPuzzleController");
		this._start = GameObject.Find("SceneStart").transform;
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			_controller.GetComponent<ObservatoryPuzzleController>().TickFail();
			GameDirector.instance.GetPlayer().gameObject.transform.position = this._start.position;
		}
	}
}
