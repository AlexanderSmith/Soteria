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
	private bool preLinger = false;
	
	public static int QTE_Delay = 3;
	private int keyPressCounter = 0;
	private float intialLinger = 1.0f;
	private float lingerDuration = 2.0f;
	private int lingerLonger = 0;
	
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
			this._inputTimer.StartTimer();
			if (preLinger)
			{
				LingerTimer();
			}
		}
		else
		{
			this.ProcessInput();			
			this.PurgeInputList();
		}
	}
	
	public int GetPressCount()
	{
		/* Multiple mash turning*********************************************
//		if (keyPressCounter >= 20 && !GameDirector.instance.GetOvercomeBool() && !preLinger)
//		{
//			GameDirector.instance.TryingToOvercome();
//			preLinger = true;
//		}
//		else if (GameDirector.instance.GetOvercomeBool())
//		{
//			GameDirector.instance.AbleToOvercome();
//		}
		//******************************************************************/
		return keyPressCounter;
	}

	private void ProcessQTEInput()
	{	
		bool inputpressed = false;

		/* Multiple mashing for turning*******************************************************************************
//		if (Input.GetKeyDown(KeyCode.DownArrow) && !GameDirector.instance.GetOvercomeBool())
//		{	
//			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer());
//			keyPressCounter++;
//			this._inputTimer.StartTimer ();
//		}
//		else if ((Input.GetKeyDown(KeyCode.Space) && keyPressCounter >= 20 && !preLinger) ||
//		         (Input.GetKeyDown(KeyCode.DownArrow) && keyPressCounter < 20 && !preLinger))
//		{
//			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer());
//			keyPressCounter++;
//			this._inputTimer.StartTimer ();
//		}
//		else
//		{
//			if (this._inputTimer.ElapsedTime() >= QTE_Delay)
//			{
//				keyPressCounter = 0;
//				preLinger = false;
//			}
//		}*/

		//*/ Single mash turning****************************************************************************************
		if (Input.GetKeyDown(KeyCode.DownArrow) && !GameDirector.instance.GetOvercomeBool())
		{
			GameDirector.instance.TryingToOvercome();
			preLinger = true;
			GameDirector.instance.BeginLingering();
			GameDirector.instance.Overpower();
			this._inputTimer.ResetTimer();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && preLinger)
		{
			inputpressed = executeInternalInput((int)ButtonType.SpaceBar, (Object) GameDirector.instance.GetPlayer());
			keyPressCounter++;
			this._inputTimer.ResetTimer();
			GameDirector.instance.ClearFromBlack();
		}
		else if (preLinger)
		{
			if (this._inputTimer.ElapsedTime() >= intialLinger)
			{
				keyPressCounter = 0;
				preLinger = false;
				GameDirector.instance.FailedToLinger();
				LingerSame();
			}
		}

		if (GameDirector.instance.GetOvercomeBool())
		{
			GameDirector.instance.AbleToOvercome();
			if (keyPressCounter > 10)
			{
				GameDirector.instance.PlayerOvercame();
				//Debug.Log("player wins");
			}
		}
		//*************************************************************************************************************/
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
			this.AddToInputList(Temp);
		
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
		if ((keyPressCounter <= 20 && !GameDirector.instance.GetOvercomeBool()) || (keyPressCounter <= 30 && GameDirector.instance.GetOvercomeBool()))
		{
			this._inputTimer.ResetTimer();
		}
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

	void LingerTimer()
	{
		lingerDuration -= Time.deltaTime;
		if (lingerDuration <= 0)
		{
			lingerLonger++;
			lingerDuration = 2.0f + lingerLonger;
			keyPressCounter = 0;
			preLinger = false;
			GameDirector.instance.NextOPStage();
			//Debug.Log("False from linger timer reset");
			GameDirector.instance.ResetLinger();
		}
	}

	void LingerSame()
	{
		lingerDuration = 2.0f + lingerLonger;
	}
}