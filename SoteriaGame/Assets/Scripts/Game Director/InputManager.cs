using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	List<KeyCode> _input = new List<KeyCode>();
	public int QTE_Delay = 3;
	private Timer _inputTimer;
	private TimersType _timertype;
	
	// Use this for initialization
	void Awake () 
	{
		this.enabled = false;
		this._timertype = TimersType.Input;

	}
	
	// Update is called once per frame
	public void Update () 
	{
		this.ProcessInput();
		if (this._inputTimer.ElapsedTime() >= this.QTE_Delay)
		{
			this._inputTimer.ResetTimer();
			this.PurgeInputList();
		}
	}

	private void ProcessInput()
	{
		bool inputpressed;

		if (inputpressed = Input.GetKeyDown( KeyCode.Space))
			this.AddToInputList(KeyCode.Space);

		if (inputpressed)
			this._inputTimer.StartTimer();
	}

	private void PurgeInputList()
	{
		if (this._input.Count > 0)
		{
			this._input.Clear();	
			this._inputTimer.StopTimer();
		}
	}

	private void AddToInputList(KeyCode inKey)
	{
		this._inputTimer.ResetTimer();
		this._input.Add(inKey);
	}
	
	public void Initialize()
	{
		this._inputTimer = TimerManager.instance.Attach(this._timertype);
	}
}
