using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	private List<Command> _buttonTypes = new List<Command>();
	private List<Button> _input = new List<Button>();
	
	private Timer _inputTimer;
	private TimersType _timertype;
	
	public static int QTE_Delay = 3;
	int keyPressCounter = 0;
	
	// Use this for initialization
	void Awake () 
	{
		this.enabled = false;
		this._timertype = TimersType.Input;
		
		//CreateButtonList//
		_buttonTypes.Add( 	new LeftCommand()  ); //LeftArrow
		_buttonTypes.Add( 	new RightCommnad() ); //RightArrow
		_buttonTypes.Add( 	new UpCommand()    ); //UpArrow
		_buttonTypes.Add(  	new DownCommand()  ); //DownArrow
		_buttonTypes.Add(   new SpaceCommand() ); //Spacebar
		
	}
	
	// Update is called once per frame
	public void Update () 
	{
		this.ProcessInput();
		if (this._inputTimer.ElapsedTime() >= QTE_Delay)
		{
			for (int i = 0; i < this._input.Count; i++)
			{
				if (this._input[i].Killit())
				{
					this._input.RemoveAt(i);
					break;
				}
			}
			
			if (_input.Count == 0)
				this._inputTimer.ResetTimer();
			//this.PurgeInputList();
		}
	}
	
	public int getPressCount()
	{
		int temp = keyPressCounter;
		//return _input.Count;
		if (keyPressCounter >= 20 && !GameDirector.instance.GetOvercomeBool())
		{
			keyPressCounter = 0;
			GameDirector.instance.TryingToOvercome();
		}
		else
		{
			GameDirector.instance.AbleToOvercome();
		}
		return temp;
	}
	
	private void ProcessInput()
	{
		bool inputpressed = false;
		
		if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)
		     || Input.GetKeyDown(KeyCode.S)) && !GameDirector.instance.GetOvercomeBool())
		{	
			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer() );
			keyPressCounter++;
		}
		else if ((Input.GetKeyDown(KeyCode.DownArrow) && keyPressCounter >= 20) ||
		        ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && keyPressCounter < 20))
		{
			keyPressCounter++;
		}
		
		if (inputpressed)
			this._inputTimer.StartTimer ();
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
		Button Temp = new Button( (ButtonType)inButtonType, _buttonTypes[inButtonType], Time.time);
		Temp.execute(inActor);
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
}