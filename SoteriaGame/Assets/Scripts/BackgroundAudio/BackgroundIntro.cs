using UnityEngine;
using System.Collections;

public class BackgroundIntro : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip (AudioID.BackgroundIntro);
	}
}