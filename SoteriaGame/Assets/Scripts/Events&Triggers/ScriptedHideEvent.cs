using UnityEngine;
using System.Collections;

public class ScriptedHideEvent : MonoBehaviour
{
	private Player _player;
	private GameObject _enemy;
	public Transform playerHidingSpot;
	public Transform enemyRunAwaySpot;
	public Transform enemyMoveToSpot;
	private bool _playerMovement;

	void Start()
	{
		_player = GameDirector.instance.GetPlayer();
		_playerMovement = false;
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GameDirector.instance.GetPlayer().PlayerActionNoFighting();
			InitializeEvent();
		}
	}

	void InitializeEvent()
	{
		_enemy = GameObject.Find("ScriptedEnemy");
		StartCoroutine("TwoSecondDelay");
	}

	void Update()
	{
		if (_enemy != null)
		{
			if (GameDirector.instance.GetGameState() != GameStates.Hidden)
			{
				_enemy.GetComponent<NavMeshAgent>().SetDestination(enemyMoveToSpot.position);
			}
			else
			{
				if (Vector3.Distance(_enemy.transform.position, enemyRunAwaySpot.position) > 3f)
				{
					_enemy.GetComponent<NavMeshAgent>().SetDestination(enemyRunAwaySpot.position);
				}
				else
				{
					Destroy(_enemy);
					GameDirector.instance.GetPlayer().PlayerActionHiding();
				}
			}
		}
		if (_playerMovement)
		{
			_player.transform.LookAt(playerHidingSpot);
			if (Vector3.Distance(_player.gameObject.transform.position, playerHidingSpot.position) > 1.5f)
			{
				_player.gameObject.transform.position = Vector3.Lerp (_player.gameObject.transform.position, playerHidingSpot.position, Time.deltaTime);
				GameDirector.instance.GetPlayer().Moving();
			}
			else
			{
				_playerMovement = false;
				_player.gameObject.transform.position = playerHidingSpot.position;
				//GameDirector.instance.GetPlayer().PlayerActionPause();
			}
		}
	}

	IEnumerator TwoSecondDelay()
	{
		yield return new WaitForSeconds(1.0f);
		_playerMovement = true;
	}
}