using UnityEngine;
using System.Collections;

public class EyeballShadowCreatureSpawner : MonoBehaviour
{
	public float spawnTime = 5.0f;
	public Transform[] spawnLocations;
	private int _spawnerIndex;
	public GameObject enemyPrefab;
	private GameObject _currentEnemy;

	void Start()
	{
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
		_currentEnemy = enemy;
	}

	public void Cancel()
	{
		this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().enabled = false;
		CancelInvoke ("ShadowCreatureSpawner");
	}

	public void Resume()
	{
		if (!GameDirector.instance.CanFight())
		{
			this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().enabled = true;
			InvokeRepeating("ShadowCreatureSpawner", 0.0f, spawnTime);
		}
		else if (_currentEnemy == null)
		{
			this.gameObject.GetComponent<EyeballShadowCreatureSpawner>().enabled = true;
			Invoke("ShadowCreatureSpawner", 0.0f);
		}
	}
}
