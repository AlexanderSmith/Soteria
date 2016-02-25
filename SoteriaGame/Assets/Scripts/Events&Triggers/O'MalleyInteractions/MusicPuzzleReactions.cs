using UnityEngine;
using System.Collections;

public class MusicPuzzleReactions : Reaction
{
	public override void execute()
	{
		if (GameDirector.instance.GetLeftKey())
		{
			GameDirector.instance.GetDialogueFromReaction("AnaDroppingItemAfterMusicPuzz", this.gameObject.transform.parent.gameObject);
		}
	}
}