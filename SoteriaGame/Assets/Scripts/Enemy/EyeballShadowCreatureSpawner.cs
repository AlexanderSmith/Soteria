using UnityEngine;
using System.Collections;

public class EyeballShadowCreatureSpawner : MonoBehaviour
{
	public float spawnTime = 5.0f;
	public Transform[] spawnLocations;
	private int _spawnerIndex;
	public GameObject enemyPrefab;

	void Start()
	{
		InvokeRepeating("ShadowCreatureSpawner", 0.0f, spawnTime);
		_spawnerIndex = 0;
	}

	private void ShadowCreatureSpawner()
	{

		if (_spawnerIndex == spawnLocations.Length - 1)
		{
			_spawnerIndex = 0;
		}
		else
		{
			_spawnerIndex++;
		}
		GameObject enemy = Instantiate (enemyPrefab, spawnLocations [_spawnerIndex].position, spawnLocations [_spawnerIndex].rotation) as GameObject;
//		enemy.GetComponent<BasicEnemyController>().Initialize(GameDirector.instance.GetEncounterManager());
//		enemy.GetComponent<BasicEnemyController>().GetAgent().SetDestination(GameDirector.instance.GetPlayer().transform.position);
	}
}
