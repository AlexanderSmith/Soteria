using UnityEngine;
using System.Collections;

public class PuppetLightEncounter : MonoBehaviour
{
	private GameObject _controller;
	public GameObject _nextLight;
	private bool _active;

	void Start()
	{
		this._controller = GameObject.Find("PuppetPuzzleController");
		this._active = false;
	}

	void Update()
	{
		if (this._active)
		{
			this.transform.position = new Vector3(GameDirector.instance.GetPlayer().transform.position.x, this.transform.position.y,
			                                      GameDirector.instance.GetPlayer().transform.position.z);
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._active = true;
			GameDirector.instance.PuppetPuzzleEncounter();
			this._controller.GetComponent<PuppetPuzzleController>().LightEncounter(_nextLight);
			this.GetComponent<SphereCollider>().enabled = false;
		}
	}
}