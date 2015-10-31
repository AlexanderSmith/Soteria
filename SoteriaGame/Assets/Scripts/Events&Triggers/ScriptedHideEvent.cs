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
		_enemy = GameObject.Find("ScriptedEnemy");
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			PlayEvent();
		}
	}

	void PlayEvent()
	{
		//while(Vector3.Distance(_player.transform.position, playerHidingSpot.position) > 
	}
}
