using UnityEngine;
using System.Collections;

public class BackgroundHarbor : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip (AudioID.BackgroundHarbor);
	}
}