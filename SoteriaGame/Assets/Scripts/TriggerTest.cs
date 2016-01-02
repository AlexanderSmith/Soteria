using UnityEngine;
using System.Collections;

public class TriggerTest : MonoBehaviour
{
	public void TriggerTesting()
	{
		GameDirector.instance.PlayAudioClip(AudioID.OMalleyMeow);
		GameDirector.instance.EndTriggerState();
	}
}
