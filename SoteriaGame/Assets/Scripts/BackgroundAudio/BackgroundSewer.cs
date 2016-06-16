using UnityEngine;
using System.Collections;

public class BackgroundSewer : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip(AudioID.BackgroundSewer);
	}
}