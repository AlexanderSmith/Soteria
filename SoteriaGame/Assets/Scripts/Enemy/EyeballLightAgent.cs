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

	void Start()
	{
		_player = GameObject.FindWithTag ("Player");
		_agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		_agent.speed = _patrolSpeed;
		if (_agent.remainingDistance < _agent.stoppingDistance)
		{
			_patrolTimer += Time.deltaTime;
			if (_patrolTimer >= waitTime)
			{
				if (_patrolIndex == patrolLocations.Length - 1)
				{
					_patrolIndex = 0;
				}
				else
				{
					_patrolIndex++;
				}
				_patrolTimer = 0.0f;
			}
		}
		
		_agent.destination = patrolLocations [_patrolIndex].position;
	}
}
