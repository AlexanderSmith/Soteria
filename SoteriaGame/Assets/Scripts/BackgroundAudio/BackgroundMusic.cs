using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip (AudioID.BackgroundMusic);
	}
}