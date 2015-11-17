using UnityEngine;
using System.Collections;

public class BackgroundTheater : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip (AudioID.BackgroundTheater);
	}
}
