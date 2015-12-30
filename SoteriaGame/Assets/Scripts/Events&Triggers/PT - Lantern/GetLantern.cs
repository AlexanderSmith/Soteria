using UnityEngine;
using System.Collections;

public class GetLantern : Reaction
{
	GameObject dreams;

	void Start()
	{
		dreams = GameObject.Find("DreamsDirectionDialogue");
	}

	public override void execute()
	{
		GameDirector.instance.LanternTrue();
		dreams.SetActive(true);
	}
}