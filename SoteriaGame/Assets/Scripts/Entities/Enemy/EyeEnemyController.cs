using UnityEngine;
using System.Collections;

public class EyeEnemyController : MonoBehaviour {

	public GameObject player;
	private EncounterManager encounterManager;
	private NavMeshAgent agent;
	private float distanceFromPlayer = 0.0f;
	private Animator anim;
	
	public Transform[] patrolLocations;
	private int _patrolIndex = 0;
	//private float _chaseSpeed = 10.0f;
	private float _patrolSpeed = 5.0f;
	private float _patrolTimer = 0.0f;
	public float waitTime = 5.0f;
	public float lookAtDist;
	public Transform start;
	public int currentLocation = 0;
	public int nextLocation = 1;
	private float startTime;
	private float journeyLength;

	// Use this for initialization
	public void Initialize(EncounterManager encMan)
	{
		encounterManager = encMan;
	//	anim = GetComponent<Animator> ();
		start = patrolLocations[0];
		this.gameObject.transform.position = start.transform.position;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

		//special cases for if it is in distance to look at player
		if(distanceFromPlayer < lookAtDist)
			lookAtPlayer();


			unaware();

	}

	public void unaware()
	{
		if (patrolLocations.Length > 0)
		{
			if(nextLocation >= patrolLocations.Length)
			{
				nextLocation = 0;
			}

			journeyLength = Vector3.Distance(patrolLocations[currentLocation].transform.position, patrolLocations[nextLocation].transform.position);
			float distCovered = (Time.time - startTime) * _patrolSpeed;
			float fracJourney = distCovered / journeyLength;

			transform.position = Vector3.Lerp(patrolLocations[currentLocation].transform.position, patrolLocations[nextLocation].transform.position, fracJourney);

			if(this.transform.position.x == patrolLocations[nextLocation].transform.position.x && this.transform.position.z == patrolLocations[nextLocation].transform.position.z)
			{
				print("Eye has arrived at " + currentLocation);
				currentLocation = nextLocation;
				nextLocation++;
				startTime = Time.time;

			}
		}

	}

	public void OverwhelmPlayer()
	{
		// Eye does not harm player, nor interfere - no unique traits for OverwhelmPlayer with EyeEnemy

	}

	public void lookAtPlayer()
	{
	//	anim.SetBool ("Alert", true);
		this.transform.LookAt(player.transform.position);
	}
}
