using UnityEngine;
using System.Collections;

public class MusicDistrictSuitObjective : MonoBehaviour
{
	public GameObject puzzle;
	public GameObject hub;

	private bool _puzzle;

	void Awake()
	{
		this._puzzle = GameDirector.instance.GetMusicPuzzleVisitedSuit();
	}

	void Start ()
	{
		//this._puzzle = true;
		if (this._puzzle)
		{
			GameDirector.instance.ChangeObjective(hub);
		}
		else
		{
			GameDirector.instance.ChangeObjective(puzzle);
		}
	}
}