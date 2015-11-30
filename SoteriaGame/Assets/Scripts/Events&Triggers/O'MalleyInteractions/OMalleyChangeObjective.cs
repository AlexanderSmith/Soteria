﻿using UnityEngine;
using System.Collections;

public class OMalleyChangeObjective : Reaction
{
	public GameObject GameObj;

	public override void execute( )
	{
		if (GameObj != null)
			GameDirector.instance.ChangeObjective(GameObj);
		else
			GameDirector.instance.ChangeObjective(GameObject.Find ("MusicStore"));
	}
}