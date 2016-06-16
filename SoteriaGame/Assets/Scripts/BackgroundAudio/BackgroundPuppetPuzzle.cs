using UnityEngine;
using System.Collections;

public class BackgroundPuppetPuzzle : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip(AudioID.BackgroundPuppetPuzzle);
	}
}