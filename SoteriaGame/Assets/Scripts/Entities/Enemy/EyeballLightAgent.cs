using UnityEngine;
using System.Collections;

public class EyeballLightAgent : MonoBehaviour
{
	private GameObject _player;
	private NavMeshAgent _agent;
	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	public float _chaseSpeed = 10.0f;
	public float _patrolSpeed = 5.0f;
	public float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	private bool _spotted;
	private bool _dialoguePause;

	void Start()
	{
		this._player = GameObject.FindWithTag ("Player");
		this._agent = GetComponent<NavMeshAgent>();
		this._spotted = false;
		this._dialoguePause = false;
	}

	void Update()
	{
		// Bad -- Needs to be re-visited later to clean up these ridiculous if blocks
		if (GameDirector.instance.GetPlayer().GetPlayerState() != PlayerState.Dialogue)
		{
			this._agent.Resume();
			if (!this._spotted)
			{
				this.Patrolling();
			}
			else if (GameDirector.instance.GetGameState() == GameStates.Hidden)
			{
				this._spotted = false;
				this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().Cancel();
				this.GetComponentInChildren<EyeballLightController>().NormalColor();
			}
			else
			{
				this.Following();
				if (this._dialoguePause)
				{
					this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().Resume();
					this._dialoguePause = false;
				}
			}
		}
		else
		{
			this._agent.Stop();
			if (this._spotted)
			{
				this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().Cancel();
				this._dialoguePause = true;
			}
		}
	}

	private void Patrolling()
	{
		this._agent.speed = this._patrolSpeed;
		if (this._agent.remainingDistance < this._agent.stoppingDistance)
		{
			this._patrolTimer += Time.deltaTime;
			if (this._patrolTimer >= this.waitTime)
			{
				if (this._patrolIndex == this.patrolLocations.Length - 1)
				{
					this._patrolIndex = 0;
				}
				else
				{
					this._patrolIndex++;
				}
				this._patrolTimer = 0.0f;
			}
		}
		
		this._agent.destination = this.patrolLocations [this._patrolIndex].position;
	}

	private void Following()
	{
		this._agent.speed = _chaseSpeed;
		this._agent.SetDestination (_player.transform.position);
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() != GameStates.Suit && GameDirector.instance.GetPlayer().GetPlayerState() != PlayerState.Dialogue)
		{
			this._spotted = true;
			this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().Resume();
			this.GetComponentInChildren<EyeballLightController>().AlarmColor();
		}
	}
}
