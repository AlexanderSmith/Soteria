using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	List<Button> _buttonTypes = new List<Button>();
	List<Button> _input = new List<Button>();

	public int QTE_Delay = 3;

	private Timer _inputTimer;
	private TimersType _timertype;
	
	// Use this for initialization
	void Awake () 
	{
		this.enabled = false;
		this._timertype = TimersType.Input;

		//CreateButtonList//
		_buttonTypes.Add( new Button( ButtonType.LeftArrow, new LeftCommand()) ); //LeftArrow
		_buttonTypes.Add( new Button( ButtonType.RightArrow, new RightCommnad()) ); //RightArrow
		_buttonTypes.Add( new Button( ButtonType.UpArrow, new UpCommand())  ); //UpArrow
		_buttonTypes.Add( new Button( ButtonType.DownArrow, new DownCommand())  ); //DownArrow

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
		bool inputpressed = false;

		if (Input.GetKeyDown( KeyCode.LeftArrow))
		{	
			inputpressed = executeInternalInput((int)ButtonType.LeftArrow, (Object) GameDirector.instance.getPlayer() );
		}

		if (inputpressed)
			this._inputTimer.StartTimer();
	}



	public void executeExternalInput(int inButtonType, Object inActor = null)
	{
		executeExternalInput(inButtonType, inActor);
	}

	private bool executeInternalInput(int inButtonType, Object inActor = null)
	{
		return executeInput(inButtonType, inActor);
	}

	private bool executeInput(int inButtonType, Object inActor = null)
	{
		this._buttonTypes[inButtonType].execute( inActor );
		this.AddToInputList(this._buttonTypes[inButtonType]);

		return true;
	}

	private void PurgeInputList()
	{
		if (this._input.Count > 0)
		{
			this._input.Clear();	
			this._inputTimer.StopTimer();
		}
	}

	private void AddToInputList(Button inButton)
	{
		this._inputTimer.ResetTimer();
		this._input.Add(inButton);
	}
	
	public void Initialize()
	{
		this._inputTimer = TimerManager.instance.Attach(this._timertype);
	}


}
