using UnityEngine;
using System.Collections;

public class PortToHubReaction : Reaction
{
	public override void execute()
	{
		GameDirector.instance.ClearAudioList();
		GameDirector.instance.CheckLantern();
		Application.LoadLevel("HubPass3");
	}
}