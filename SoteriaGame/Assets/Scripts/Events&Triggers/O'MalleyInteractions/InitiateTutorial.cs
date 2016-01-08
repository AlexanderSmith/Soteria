using UnityEngine;
using System.Collections;

public class InitiateTutorial : MonoBehaviour
{
	void Start()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("OMalleyTeachingAnaToOvercomeFear");
		GameDirector.instance.StartDialogue();
	}
}