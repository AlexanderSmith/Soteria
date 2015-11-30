using UnityEngine;
using System.Collections;

public class GetCompass : Reaction
{
	public override void execute ()
	{
		GameDirector.instance.CompassTrue();
	}
}
