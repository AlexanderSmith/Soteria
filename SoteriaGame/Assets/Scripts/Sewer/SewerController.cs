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
	private GameObject _boss;

	Timer xformTimer;
	float xformLength;
	float deathLength;

	void Start()
	{
		xformTimer = TimerManager.instance.Attach(TimersType.OicysTimer);
		xformLength = 5.0f;
		deathLength = 5.0f;
		this._encCounter = 1;
		GameObject boss = Instantiate(oicys, oicysSpawn.transform.position, oicysSpawn.transform.rotation) as GameObject;
		_boss = boss;
	}

	void Update()
	{
		if (_boss.Equals(GameObject.FindWithTag("Xform1")))
		{
			if (xformTimer.ElapsedTime() >= xformLength)
			{
				xformTimer.StopTimer();
				xformTimer.ResetTimer();
				Destroy(_boss);
				GameObject boss4 = Instantiate(idle1, idle1Spawn.transform.position, idle1Spawn.transform.rotation) as GameObject;
				_boss = boss4;
			}
		}
		if (_boss.Equals(GameObject.FindWithTag("Xform2")))
		{
			if (xformTimer.ElapsedTime() >= xformLength)
			{
				xformTimer.StopTimer();
				xformTimer.ResetTimer();
				Destroy(_boss);
				GameObject boss5 = Instantiate(idle2, idle2Spawn.transform.position, idle2Spawn.transform.rotation) as GameObject;
				_boss = boss5;
			}
		}
		if (_boss.Equals(GameObject.FindWithTag("Death")))
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			if (xformTimer.ElapsedTime() >= deathLength)
			{
				xformTimer.StopTimer();
				xformTimer.ResetTimer();
				GameDirector.instance.EndDialogue();
				GameDirector.instance.GetPlayer().PlayerActionPause();
				GameDirector.instance.GetDialogueFromReaction("AnaFinal", this.transform.parent.gameObject);
			}
		}
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
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.GetDialogueFromReaction("AnaOicysE1Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE1D1", "AnaOicysE1D2", "AnaOicysE1D3");
	}

	public void Encounter2()
	{
		GameDirector.instance.EndDialogue();
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.GetDialogueFromReaction("AnaOicysE2Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE2D1", "AnaOicysE2D2", "AnaOicysE2D3");
	}

	public void Encounter3()
	{
		GameDirector.instance.EndDialogue();
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.GetDialogueFromReaction("AnaOicysE3Choices", this.transform.parent.gameObject);
		GameDirector.instance.SetupDialogueChoices("AnaOicysE3D1", "AnaOicysE3D2", "AnaOicysE3D3");
	}

	public void FightOicys()
	{
		GameDirector.instance.StartOicysEncounter(this);
		GameDirector.instance.EndTriggerState();
	}

	public void EncounterWon()
	{
		this._encCounter++;
		switch (this._encCounter)
		{
		case 2:
			Destroy(_boss);
			GameObject boss1 = Instantiate (xform1, xform1Spawn.transform.position, xform1Spawn.transform.rotation) as GameObject;
			xformTimer.StartTimer();
			_boss = boss1;
			this.Encounter2();
			break;
		case 3:
			Destroy(_boss);
			GameObject boss2 = Instantiate (xform2, xform2Spawn.transform.position, xform2Spawn.transform.rotation) as GameObject;
			xformTimer.StartTimer();
			_boss = boss2;
			this.Encounter3();
			break;
		case 4:
			Destroy(_boss);
			GameObject boss6 = Instantiate (death, deathSpawn.transform.position, deathSpawn.transform.rotation) as GameObject;
			xformTimer.StartTimer();
			_boss = boss6;
			break;
		}
	}

	void ResetCounter()
	{
		this._encCounter = 1;
		Destroy(_boss);
		GameObject boss3 = Instantiate(oicys, oicysSpawn.transform.position, oicysSpawn.transform.rotation) as GameObject;
		_boss = boss3;
	}

	public void EndGame()
	{
		Application.LoadLevel("Credits");
	}
}