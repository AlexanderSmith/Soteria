using UnityEngine;
using System.Collections;

public class BackgroundObsPuzzle : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip(AudioID.BackgroundObsPuzzle);
	}
}