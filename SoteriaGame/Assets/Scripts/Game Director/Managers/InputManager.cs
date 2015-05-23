using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	private List<Command> _buttonTypes = new List<Command>();
	private List<Button> _input = new List<Button>();
	
	private Timer _inputTimer;
	private TimersType _timertype;
	private bool _isqtemode;
	
	public static int QTE_Delay = 3;
	int keyPressCounter = 0;
	
	// Use this for initialization
	void Awake () 
	{
		this.enabled = false;
		this._timertype = TimersType.Input;
		this._isqtemode = false;

		//CreateButtonList//
		_buttonTypes.Add( 	new LeftCommand()  ); //LeftArrow 	(A)
		_buttonTypes.Add( 	new RightCommand() ); //RightArrow 	(D)
		_buttonTypes.Add( 	new UpCommand()    ); //UpArrow		(W)
		_buttonTypes.Add(  	new DownCommand()  ); //DownArrow	(S)
		_buttonTypes.Add(   new SpaceCommand() ); //Spacebar	
		
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if (this.isQTEMode())
		{
			this.ProcessQTEInput();

//			if (this._inputTimer.ElapsedTime() >= QTE_Delay)
//			{
//				for (int i = 0; i < this._input.Count; i++)
//				{
//					if (this._input[i].Killit())
//					{
//						this._input.RemoveAt(i);
//						break;
//					}
//				}
//				
//				if (_input.Count == 0)
//					this._inputTimer.ResetTimer();
//			}
		}
		else
		{

			this.ProcessInput();
			
			this.PurgeInputList();
		}
	}
	
	public int getPressCount()
	{
		//return _input.Count;
		int temp = keyPressCounter;
		if (keyPressCounter >= 20 && !GameDirector.instance.GetOvercomeBool())
		{
			Debug.Log("resetting key press counter");
			keyPressCounter = 0;
			GameDirector.instance.TryingToOvercome();
		}
		else
		{
			GameDirector.instance.AbleToOvercome();
		}
		return temp;
	}

	private void ProcessQTEInput()
	{	
		bool inputpressed = false;

		if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)
		     || Input.GetKeyDown(KeyCode.S)) && !GameDirector.instance.GetOvercomeBool())
		{	
			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer() );
			keyPressCounter++;
			this._inputTimer.StartTimer ();
		}
		else if ((Input.GetKeyDown(KeyCode.DownArrow) && keyPressCounter >= 20) ||
		         ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && keyPressCounter < 20))
		{
			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer() );
			keyPressCounter++;
			this._inputTimer.StartTimer ();
		}
		else
		{
			if (this._inputTimer.ElapsedTime() >= QTE_Delay)
			{
				keyPressCounter = 0;
			}
		}
	}
	private void ProcessInput()
	{
		ButtonType buttonType = ButtonType.None;

		if (Input.GetKey( KeyCode.W))
			buttonType = ButtonType.UpArrow;
		if (Input.GetKey( KeyCode.S))
			buttonType = ButtonType.DownArrow;
		if (Input.GetKey( KeyCode.A))
			buttonType = ButtonType.LeftArrow;
		if (Input.GetKey( KeyCode.D))
			buttonType = ButtonType.RightArrow;

		if (buttonType != ButtonType.None)
			executeInternalInput((int)buttonType, (Object) GameDirector.instance.GetPlayer() );
	}

	//Use it to process input from outside the Input Manager
	public void executeExternalInput(int inButtonType, Object inActor = null)
	{
		executeExternalInput(inButtonType, inActor);
	}

	//Use it to process input from Inside the Input Manager
	private bool executeInternalInput(int inButtonType, Object inActor = null)
	{
		return executeInput(inButtonType, inActor);
	}
	
	private bool executeInput(int inButtonType, Object inActor = null)
	{
		Button Temp = new Button( (ButtonType)inButtonType, _buttonTypes[inButtonType], Time.time);
		Temp.execute(inActor);

		if (this.isQTEMode())
			this.AddToInputList( Temp );
		
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

	public bool isQTEMode()
	{
		if (GameDirector.instance.GetGameState() == GameStates.Encounter && 
		    GameDirector.instance.GetEncounterState() != EncounterState.ActiveLight)
			this._isqtemode = true;
		else
			this._isqtemode = false;	

		return this._isqtemode;
	}
}