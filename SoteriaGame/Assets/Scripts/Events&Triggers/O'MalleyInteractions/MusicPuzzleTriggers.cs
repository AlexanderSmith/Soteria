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
//		GameDirector.instance.EndTriggerState();
//		GameDirector.instance.LeftKeyAcquired();
	}

	public void KillMusic()
	{
		GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 0.1f);
		GameDirector.instance.ChangeVolume(AudioID.BrassMusic, 0.1f);
		GameDirector.instance.ChangeVolume(AudioID.WindMusic, 0.1f);
		GameDirector.instance.ChangeVolume(AudioID.StringMusic, 0.1f);
	}

	public void ResumeMusic()
	{
		GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 1f);
		GameDirector.instance.ChangeVolume(AudioID.BrassMusic, 1f);
		GameDirector.instance.ChangeVolume(AudioID.WindMusic, 1f);
		GameDirector.instance.ChangeVolume(AudioID.StringMusic, 1f);
	}
}
