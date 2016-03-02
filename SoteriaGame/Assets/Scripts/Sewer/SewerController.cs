using UnityEngine;
using System.Collections;

public class SewerController : MonoBehaviour
{
	public GameObject oicys;
	public GameObject idle1;
	public GameObject idle2;
	public GameObject xform1;
	public GameObject xform2;
	public GameObject death;
	public Transform oicysSpawn;
	public Transform idle1Spawn;
	public Transform idle2Spawn;
	public Transform xform1Spawn;
	public Transform xform2Spawn;
	public Transform deathSpawn;

	private int _encCounter;

	void Start()
	{
		this._encCounter = 1;
	}

	public void StartEncounter()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("AnaOicysE1Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE1D1", "AnaOicysE1D2", "AnaOicysE1D3");
		GameDirector.instance.StartDialogue();
	}

	public void Encounter1()
	{
		this.ResetCounter();
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

	public void FightOicys()
	{
		GameDirector.instance.StartOicysEncounter(this);
	}

	public void EncounterWon()
	{
		this._encCounter++;
		switch (this._encCounter)
		{
		case 1:
			break;
		case 2:
			break;
		}
	}

	void ResetCounter()
	{
		this._encCounter = 1;
	}
}