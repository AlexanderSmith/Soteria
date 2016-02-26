using UnityEngine;
using System.Collections;

public class DefeatedObsPuzzleReaction : Reaction
{
	private GameObject _controller;
	
	void Start()
	{
		this._controller = GameObject.Find("ObsPuzzleController");
	}
	
	public override void execute ()
	{
		this._controller.GetComponent<ObservatoryPuzzleController>().SpawnOMalleyAfterKeyPiece();
	}
}