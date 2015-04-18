using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	List<KeyCode> _input = new List<KeyCode>();
	public int QTE_Delay = 3;
	private Timer _inputTimer;
	
	// Use this for initialization
	void Awake () 
	{
		this.enabled = false;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		ProcessInput();
		if (_inputTimer.ElapsedTime() >= QTE_Delay)
		{
			_inputTimer.ResetTimer();
			PurgeInputList();
		}
	}

	private void ProcessInput()
	{
		bool inputpressed;

		if (inputpressed = Input.GetKeyDown( KeyCode.Space))
			this.AddToInputList(KeyCode.Space);

		if (inputpressed)
			_inputTimer.StartTimer();
	}

	private void PurgeInputList()
	{
		if (_input.Count > 0)
		{
			_input.Clear();	
			_inputTimer.StopTimer();
		}
	}

	private void AddToInputList(KeyCode inKey)
	{
		_inputTimer.ResetTimer();
		_input.Add(inKey);
	}
	
	public void Initialize()
	{
		_inputTimer = this.gameObject.GetComponent<Timer>();

	}
}
