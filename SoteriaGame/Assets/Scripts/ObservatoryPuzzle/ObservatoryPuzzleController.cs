using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObservatoryPuzzleController : MonoBehaviour
{
	private GameObject _doorEncounter1;
	private GameObject _doorEncounter2;
	private GameObject _doorEncounter3;
	private GameObject _reset;
	private GameObject _oMalley;
	private GameObject _oMalleyPuzzleDefeated;
	private GameObject _midKey;

	public GameObject tileUp;
	public GameObject tileDown;
	public GameObject tileLeft;
	public GameObject tileRight;
	public GameObject tileCtr;
	public GameObject lastTile;

	private bool _offPath;
	private bool _activated;
	private int _timesFailed;

	private int _doorEncountersWon;

	private GameObject[] _lights;

	private WorldFlags worldFlagState 	= WorldFlags.EMPTY_FLAG;

	// Use this for initialization
	void Awake()
	{
		//Hack for suit
//		GameDirector.instance.SuitWorn();
//		GameDirector.instance.ObsPuzzleActivated();
//		GameDirector.instance.SuitRemoved();

		worldFlagState = ProgressionManager.instance.Flags_World;

		tileUp.GetComponentInChildren<Light>().enabled = false;
		tileDown.GetComponentInChildren<Light>().enabled = false;
		tileLeft.GetComponentInChildren<Light>().enabled = false;
		tileRight.GetComponentInChildren<Light>().enabled = false;
		tileCtr.GetComponentInChildren<Light>().enabled = false;
		_doorEncounter1 = GameObject.Find("DoorEncounter1");
		_doorEncounter1.SetActive(false);
		_doorEncounter2 = GameObject.Find("DoorEncounter2");
		_doorEncounter2.SetActive(false);
		_doorEncounter3 = GameObject.Find("DoorEncounter3");
		_doorEncounter3.SetActive(false);
		this._lights = GameObject.FindGameObjectsWithTag("TileLight");
		this._timesFailed = 0;
		this._doorEncountersWon = 0;
		this._reset = GameObject.Find("PortToEntrance");
		this._oMalley = GameObject.Find("O'Malley3Fails");
		this._oMalley.SetActive(false);
		this._activated = false;

		if (FlagTools.World_CheckFlag(worldFlagState, WorldFlags.ITEM_SUIT))
		{
			foreach (GameObject light in _lights)
			{
				light.GetComponentInChildren<Light>().enabled = true;
				light.GetComponent<BoxCollider>().enabled = false;
			}
		}

		this._oMalleyPuzzleDefeated = GameObject.Find("O'MalleyObservatoryPuzzleDefeated");
		this._oMalleyPuzzleDefeated.SetActive(false);
		this._midKey = GameObject.Find ("KeyPieceInspect");
		this._midKey.GetComponentInChildren<SphereCollider>().enabled = false;

		// Testing
		//GameDirector.instance.SuitRemoved();
	}

	public void Initialize()
	{
		// Hacks for fighting puzzle
//		GameDirector.instance.ObsPuzzleActivated();
//		GameDirector.instance.SuitRemoved();

		if (!FlagTools.World_CheckFlag(worldFlagState, WorldFlags.HUB_PHASE4) && !FlagTools.World_CheckFlag(worldFlagState, WorldFlags.ITEM_SUIT))
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaEnteringObservPuzzFirstTime");
			GameDirector.instance.StartDialogue(true);
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive() && !FlagTools.World_CheckFlag(worldFlagState, WorldFlags.HUB_PHASE4))
			{
				GameDirector.instance.ObsPuzzleActivated();
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
			if (!GameDirector.instance.isDialogueActive() && this._timesFailed == 1)
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
	
	public void EnableTileUpLight()
	{
		tileUp.GetComponentInChildren<Light>().enabled = true;
	}

	public void EnableTileDownLight()
	{
		tileDown.GetComponentInChildren<Light>().enabled = true;
	}

	public void EnableTileLeftLight()
	{
		tileLeft.GetComponentInChildren<Light>().enabled = true;
	}

	public void EnableTileRightLight()
	{
		tileRight.GetComponentInChildren<Light>().enabled = true;
	}

	public void DisableTileUpLight()
	{
		tileUp.GetComponentInChildren<Light>().enabled = false;
	}
	
	public void DisableTileDownLight()
	{
		tileDown.GetComponentInChildren<Light>().enabled = false;
	}
	
	public void DisableTileLeftLight()
	{
		tileLeft.GetComponentInChildren<Light>().enabled = false;
	}
	
	public void DisableTileRightLight()
	{
		tileRight.GetComponentInChildren<Light>().enabled = false;
	}

	public void DisableAllLights()
	{
		tileCtr.GetComponentInChildren<Light>().enabled = false;
		this.DisableTileUpLight();
		this.DisableTileDownLight();
		this.DisableTileLeftLight();
		this.DisableTileRightLight();
	}

	public void EnableTileCtrLight()
	{
		tileCtr.GetComponentInChildren<Light>().enabled = true;
	}

	public void OffPath()
	{
		this._offPath = true;
		this.lastTile.GetComponent<BoxCollider>().enabled = false;
		GameDirector.instance.ChangeVolume(AudioID.Whispers, 0.5f);
	}

	public bool GetOffPath()
	{
		return this._offPath;
	}

	public bool GetActivated()
	{
		return this._activated;
	}

	public void Activated()
	{
		this._activated = true;
	}

	public void EnableDoorEncounters()
	{
		_doorEncounter1.SetActive(true);
		_doorEncounter2.SetActive(true);
		_doorEncounter3.SetActive(true);
	}

	public void DoorEncounterWon()
	{
		this._doorEncountersWon++;
		GameDirector.instance.ChangeVolume(AudioID.Whispers, 0);
		if (this._doorEncountersWon > 3)
		{
			this._reset.GetComponent<BoxCollider>().enabled = false;
			GameDirector.instance.ObservatoryPuzzleDefeated();
			this._midKey.GetComponentInChildren<SphereCollider>().enabled = true;
		}
	}

	public void TickFail()
	{
		if (!FlagTools.World_CheckFlag(worldFlagState, WorldFlags.ITEM_SUIT))
		{
			foreach(GameObject light in this._lights)
			{
				light.GetComponentInChildren<Light>().enabled = false;
				light.GetComponent<BoxCollider>().enabled = true;
			}
		}

		this._doorEncountersWon = 0;
		this._timesFailed++;
		
		if (this._timesFailed == 1 && FlagTools.World_CheckFlag(worldFlagState, WorldFlags.ITEM_SUIT))
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaObservPuzzAtDoorSuit");
			GameDirector.instance.StartDialogue();
		}
		else if (this._timesFailed == 3 && !FlagTools.World_CheckFlag(worldFlagState, WorldFlags.ITEM_SUIT) && GameDirector.instance.GetGamePhase() < 4)
		{
			this._oMalley.SetActive(true);
		}
	}

	public void SpawnOMalleyAfterKeyPiece()
	{
		this._oMalleyPuzzleDefeated.SetActive(true);
	}
}