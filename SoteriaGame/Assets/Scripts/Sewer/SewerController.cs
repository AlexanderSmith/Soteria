using UnityEngine;
using System.Collections;

public class SewerController : MonoBehaviour
{
	public void StartEncounter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("AnaOicysE1Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE1D1", "AnaOicysE1D2", "AnaOicysE1D3");
		GameDirector.instance.StartDialogue();
	}

	public void Encounter1()
	{
		GameDirector.instance.EndDialogue();
		GameDirector.instance.GetDialogueFromReaction("AnaOicysE1Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE1D1", "AnaOicysE1D2", "AnaOicysE1D3");
	}

	public void Encounter2()
	{
		GameDirector.instance.EndDialogue();
		GameDirector.instance.GetDialogueFromReaction("AnaOicysE2Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE2D1", "AnaOicysE2D2", "AnaOicysE2D3");
	}

	public void Encounter3()
	{
		GameDirector.instance.EndDialogue();
		GameDirector.instance.GetDialogueFromReaction("AnaOicysE3Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE3D1", "AnaOicysE3D2", "AnaOicysE3D3");
	}
}