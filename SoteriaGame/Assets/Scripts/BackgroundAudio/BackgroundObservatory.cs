using UnityEngine;
using System.Collections;

public class BackgroundObservatory : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip (AudioID.BackgroundObservatory);
	}
}