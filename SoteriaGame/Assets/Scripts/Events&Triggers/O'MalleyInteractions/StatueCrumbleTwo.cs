﻿using UnityEngine;
using System.Collections;

public class StatueCrumbleTwo : Reaction
{
	public override void execute()
	{
		GameDirector.instance.StatueCrumbleTwo();
		if (GameDirector.instance.GetCompass())
		{
			GameDirector.instance.ChangeObjective(GameObject.Find ("PortToHub"));
		}
		GameDirector.instance.GetDialogueFromReaction("OMalleyHubp5BeforeObservatoryPuzzle", this.gameObject.transform.parent.gameObject);
	}
}