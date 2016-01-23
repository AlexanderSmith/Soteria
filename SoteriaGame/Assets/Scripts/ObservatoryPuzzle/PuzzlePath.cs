using UnityEngine;
using System.Collections;

public enum ActiveLights
{
	up,
	down,
	left,
	right,
	none
};

struct CurrentPath
{
	public ActiveLights next;
	public ActiveLights prev;
}

public class PuzzlePath : MonoBehaviour 
{
	public ActiveLights path1next;
	public ActiveLights path1prev;
	public ActiveLights path2next;
	public ActiveLights path2prev;
	public ActiveLights path3next;
	public ActiveLights path3prev;

	public GameObject tileUp;
	public GameObject tileDown;
	public GameObject tileLeft;
	public GameObject tileRight;

	private ObservatoryPuzzleController _controller;

	CurrentPath _currentPath;

	void Start()
	{
		_currentPath.next = path1next;
		_currentPath.prev = path1prev;
		_controller = GameObject.Find("ObsPuzzleController").GetComponent<ObservatoryPuzzleController>();
		if (GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			this.GetComponentInChildren<Light>().enabled = false;
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (GameDirector.instance.GetGameState() != GameStates.Suit)
		{
			if (player.gameObject.tag == "Player" && !this._controller.GetOffPath())
			{
				this.GetComponentInChildren<Light>().enabled = true;
				if (_currentPath.next != ActiveLights.none)
				{
					if (_currentPath.next == ActiveLights.up || _currentPath.prev == ActiveLights.up)
					{
						this._controller.EnableTileUpLight();
						//this.GetComponentInChildren<Light>().enabled = true;
					}
					else
					{
						this._controller.DisableTileUpLight();
					}
					if (_currentPath.next == ActiveLights.down || _currentPath.prev == ActiveLights.down)
					{
						this._controller.EnableTileDownLight();
						//this.GetComponentInChildren<Light>().enabled = true;
					}
					else
					{
						this._controller.DisableTileDownLight();
					}
					if (_currentPath.next == ActiveLights.left || _currentPath.prev == ActiveLights.left)
					{
						this._controller.EnableTileLeftLight();
						//this.GetComponentInChildren<Light>().enabled = true;
					}
					else
					{
						this._controller.DisableTileLeftLight();
					}
					if (_currentPath.next == ActiveLights.right || _currentPath.prev == ActiveLights.right)
					{
						this._controller.EnableTileRightLight();
						//this.GetComponentInChildren<Light>().enabled = true;
					}
					else
					{
						this._controller.DisableTileRightLight();
					}
				}
				else
				{
					this._controller.OffPath();
					this._controller.DisableAllLights();
					this._controller.EnableDoorEncounters();
					GameDirector.instance.ObsPuzzleEncounter();
				}
			}
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Debug.Log ("FML");
		}
	}

//	void OnTriggerExit(Collider player)
//	{
//		if (player.gameObject.tag == "Player" && GameDirector.instance.GetGameState() != GameStates.Suit)
//		{
//			this.GetComponentInChildren<Light>().enabled = false;
//		}
//	}
}