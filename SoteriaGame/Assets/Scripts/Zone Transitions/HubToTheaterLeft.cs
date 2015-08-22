﻿using UnityEngine;
using System.Collections;

public class HubToTheaterLeft : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Application.LoadLevel("WalkThroughTheater");
		}
	}
}