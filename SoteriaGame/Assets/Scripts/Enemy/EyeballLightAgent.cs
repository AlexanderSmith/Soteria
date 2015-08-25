using UnityEngine;
using System.Collections;

public class EyeballLightAgent : MonoBehaviour
{
	private GameObject _player;
	private NavMeshAgent _agent;
	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	private float _chaseSpeed = 10.0f;
	private float _patrolSpeed = 5.0f;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	private bool _spotted;

	void Start()
	{
		this._player = GameObject.FindWithTag ("Player");
		this._agent = GetComponent<NavMeshAgent>();
		this._spotted = false;
	}

	void Update()
	{
		if (!this._spotted)
		{
			this.Patrolling();
		}
		else
		{
			this.Following();
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
		if (player.gameObject.tag == "Player")
		{
			this._spotted = true;
			this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().enabled = true;
		}
	}
}
