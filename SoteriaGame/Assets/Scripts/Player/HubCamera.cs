using UnityEngine;
using System.Collections;

public class HubCamera : MonoBehaviour
{
	public Transform player;
	public float smooth;

	// Use this for initialization
	void Start ()
	{
		if (player == null)
		{
			player = GameObject.FindWithTag ("Player").transform;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (player);
	}
}
