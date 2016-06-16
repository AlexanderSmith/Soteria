using UnityEngine;
using System.Collections;

public class BackgroundHub : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.PlayAudioClip (AudioID.BackgroundHub);
	}
}