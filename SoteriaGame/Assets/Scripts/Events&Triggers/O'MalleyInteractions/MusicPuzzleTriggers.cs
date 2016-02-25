using UnityEngine;
using System.Collections;

public class MusicPuzzleTriggers : MonoBehaviour
{
	public GameObject keyPiece;
	public Sprite _leftKey;

	public void LeftKeyPiece()
	{
		GameDirector.instance.ChangeObjective(null);
		this.keyPiece.SetActive(false);
		GameDirector.instance.StartKeyInteraction(this._leftKey);
		GameDirector.instance.EndTriggerState();
		GameDirector.instance.LeftKeyAcquired();
	}
}
