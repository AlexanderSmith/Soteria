using UnityEngine;
using System.Collections;

public class HubMovements : MonoBehaviour
{
	private NavMeshAgent _agent;
	private Animator _anim;
	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	
	
	// Use this for initialization
	void Start ()
	{
		_agent = GetComponent<NavMeshAgent>();
		_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameDirector.instance.isDialogueActive())
		{
			_agent.Resume();
			if (patrolLocations.Length > 0)
			{
				_anim.SetBool ("Moving", true);
				if (_agent.remainingDistance < _agent.stoppingDistance)
				{
					_anim.SetBool ("Moving", false);
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
			else
			{
				_anim.SetBool ("Moving", false);
			}
		}
		else
		{
			_anim.SetBool ("Moving", false);
			_patrolTimer = 0.0f;
			_agent.Stop();
		}
	}
}