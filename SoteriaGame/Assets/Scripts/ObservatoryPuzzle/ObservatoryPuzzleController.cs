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

	public GameObject tileUp;
	public GameObject tileDown;
	public GameObject tileLeft;
	public GameObject tileRight;
	public GameObject tileCtr;
	public GameObject lastTile;

	private bool _offPath;
	private int _timesFailed;

	private int _doorEncountersWon;

	private GameObject[] _lights;

	// Use this for initialization
	void Awake()
	{
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

		// Testing
		//GameDirector.instance.SuitRemoved();
	}

	public void Initialize()
	{
		if (GameDirector.instance.ObsPuzzleActivated())
		{
		}
		else
		{
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaEnteringObservPuzzFirstTime");
			GameDirector.instance.StartDialogue();
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
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
	}

	public bool GetOffPath()
	{
		return this._offPath;
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
		if (this._doorEncountersWon >= 3)
		{
			this._reset.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void TickFail()
	{
		foreach(GameObject light in this._lights)
		{
			light.GetComponentInChildren<Light>().enabled = false;
			light.GetComponent<BoxCollider>().enabled = true;
		}
		this._doorEncountersWon = 0;
		this._timesFailed++;
		if (this._timesFailed == 3)
		{
			this._oMalley.SetActive(true);
		}
	}
}