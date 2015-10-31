using UnityEngine;
using System.Collections;

public class ScriptedHideEvent : MonoBehaviour
{
	private Player _player;
	private GameObject _enemy;
	public Transform playerHidingSpot;
	public Transform enemyRunAwaySpot;

	void Start()
	{
		_player = GameDirector.instance.GetPlayer();
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			InitializeEvent();
		}
	}

	void InitializeEvent()
	{
		_enemy = GameObject.Find("ScriptedEnemy");
	}

	void Update()
	{
		if (_enemy != null)
		{

		}
	}
}
