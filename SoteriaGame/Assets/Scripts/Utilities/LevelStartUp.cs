using UnityEngine;
using System.Collections;

public class LevelStartUp : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies)
		{
			enemy.SetActive(true);
		}
	}
}
