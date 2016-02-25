using UnityEngine;
using System.Collections;

public class DefeatedPuppetReaction : Reaction
{
	private GameObject _controller;
	
	void Start()
	{
		this._controller = GameObject.Find("PuppetPuzzleController");
	}
	
	public override void execute ()
	{
		this._controller.GetComponent<PuppetPuzzleController>().SpawnOMalleyAfterPuzzleDefeated();
	}
}