using UnityEngine;
using System.Collections;

public class MusicPuzzleReactions : Reaction
{
	public override void execute()
	{
		if (GameDirector.instance.GetLeftKey())
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.StartFadeInTimer ();
			Debug.Log ("Starting Fade In Timer");
		}
	}

	public void Update()
	{
		GameDirector.instance.FadeIn (AudioID.OrganMusic);
		GameDirector.instance.FadeIn (AudioID.BrassMusic);
		GameDirector.instance.FadeIn (AudioID.WindMusic);
		GameDirector.instance.FadeIn (AudioID.StringMusic);
	}
}