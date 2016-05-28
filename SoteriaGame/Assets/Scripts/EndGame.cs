using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
	void Awake()
	{
		GameDirector.instance.EndGameImageOn();
	}

	void Start ()
	{
		GameDirector.instance.SetupDialogue("AnaFinal");
		GameDirector.instance.StartDialogue();
	}
}
