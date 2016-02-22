using UnityEngine;
using System.Collections;

public class InitiateTutorial : MonoBehaviour
{
	private bool _start;
	private bool _fail;
	private bool _success;
	private bool _lingering;

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
		this.ResetLinger();
	}

	public void Succeeded()
	{
		this._success = true;
	}

	public void ResetFail()
	{
		this._fail = false;
	}

	public bool GetFail()
	{
		return this._fail;
	}

	public bool GetSuccess()
	{
		return this._success;
	}

	public bool GetLingering()
	{
		return this._lingering;
	}

	public void ResetLinger()
	{
		this._lingering = false;
	}

	public void Lingering()
	{
		this._lingering = true;
	}
}