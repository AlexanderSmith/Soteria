using UnityEngine;
using System.Collections;

public class InitiateTutorial : MonoBehaviour
{
	private bool _start;
	private bool _fail;
	private bool _success;

	void Start()
	{
		GameDirector.instance.GetPlayer().PlayerActionPause();
		GameDirector.instance.SetupDialogue("OMalleyTeachingAnaToOvercomeFear1");
		GameDirector.instance.StartDialogue();
		this._start = true;
	}

	public void Failed()
	{
		this._fail = true;
	}

	public void Succeeded()
	{
		this._success = true;
	}

	public bool GetFail()
	{
		return this._fail;
	}

	public bool GetSuccess()
	{
		return this._success;
	}
}