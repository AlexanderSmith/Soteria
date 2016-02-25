using UnityEngine;
using System.Collections;

public class MusicPuzzleKeyPiece : Reaction
{
	public GameObject keyPiece;
	public Sprite _leftKey;

	public override void execute()
	{
		GameDirector.instance.ChangeObjective(null);
		this.keyPiece.SetActive(false);
		GameDirector.instance.StartKeyInteraction(this._leftKey);
	}
}