using UnityEngine;
using System.Collections;

public enum ActiveLights
{
	up,
	down,
	left,
	right
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

	CurrentPath _currentPath;

	void Start()
	{
		_currentPath.next = path1next;
		_currentPath.prev = path1prev;
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			{
				if (_currentPath.next == ActiveLights.up || _currentPath.prev == ActiveLights.up)
				{
					tileUp.GetComponentInChildren<Light>().enabled = true;
				}
				else
				{
					tileUp.GetComponentInChildren<Light>().enabled = false;
				}
				if (_currentPath.next == ActiveLights.down || _currentPath.prev == ActiveLights.down)
				{
					tileDown.GetComponentInChildren<Light>().enabled = true;
				}
				else
				{
					tileDown.GetComponentInChildren<Light>().enabled = false;
				}
				if (_currentPath.next == ActiveLights.left || _currentPath.prev == ActiveLights.left)
				{
					tileLeft.GetComponentInChildren<Light>().enabled = true;
				}
				else
				{
					tileLeft.GetComponentInChildren<Light>().enabled = false;
				}
				if (_currentPath.next == ActiveLights.right || _currentPath.prev == ActiveLights.right)
				{
					tileRight.GetComponentInChildren<Light>().enabled = true;
				}
				else
				{
					tileRight.GetComponentInChildren<Light>().enabled = false;
				}
			}
		}
	}
}