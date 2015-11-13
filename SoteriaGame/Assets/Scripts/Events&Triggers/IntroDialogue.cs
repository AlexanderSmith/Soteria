using UnityEngine;
using System.Collections;

public class IntroDialogue : MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameDirector.instance.SetupDialogue ("IntroDialogue", AudioID.None);
		GameDirector.instance.StartDialogue ();
	}
}
