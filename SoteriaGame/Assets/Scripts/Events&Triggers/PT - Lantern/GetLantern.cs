using UnityEngine;
using System.Collections;

public class GetLantern : Reaction
{
	public override void execute()
	{
		GameDirector.instance.LanternTrue();
	}
}