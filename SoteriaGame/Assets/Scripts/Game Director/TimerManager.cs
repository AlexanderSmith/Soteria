using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Basic Timer class, it can be used for pretty much anything
//Inputs will later use "coroutines" so for now we'll keep this but we should swap it out later.

/// <summary>
/// Set a Name for each Timer we need. These will also function as the index of the list in the manager.
/// </summary>
public enum TimersType
{
	Generic = 0,
	Input,
	Puzzle
}
/// <summary>
/// The Actual manager that takes care of the Timers.
/// This class is technically an exception, it hands out pointers to Timers it doesn't need to go throught the Game Director
/// </summary>
public class TimerManager: MonoBehaviour
{
	private static TimerManager _instance;
	private List<Timer> _timersList = new List<Timer>();

	public static TimerManager instance
	{
		get {
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<TimerManager>();
			return (TimerManager)(_instance);
		}
	}

	void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
			this.Initialize();
			DontDestroyOnLoad(this); //Keep the instance going between scenes
		}
		else
		{
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	private void Initialize()
	{
		this.enabled = false; //force update from game director no matter what.
		int count = Enum.GetNames(typeof(TimersType)).Length;
		_timersList.Capacity = count; //Set the Size (it cannot be modified)
		for (int i=0; i<count; ++i)
			_timersList[i] = new Timer();
			
	}

	public void Update ()
	{
		foreach (Timer t in _timersList)
			t.Update();
	}

	//Use this to attach it to a specific class ( a class that has a 
	public Timer Attach(TimersType intype)
	{
		return this._timersList[(int)intype];
	}
}

/// <summary>
/// The timer class is just a timer that wraps the Time class.
/// This class could be nested but I'm not a big fan of that approach
/// </summary>
public class Timer
{
	private float elapsedTime; //Elapsed time from last Input;
	private bool started;
	
	// Use this for initialization
	public Timer()
	{
		this.started = false;
	}

	public void StartTimer()
	{
		this.started = true;
	}

	public void StopTimer()
	{
		this.ResetTimer();
		this.started = false;
	}

	public float ElapsedTime()
	{
		return this.elapsedTime;
	}

	public void ResetTimer()
	{
		this.elapsedTime = 0;
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (this.started)
			this.elapsedTime += Time.deltaTime;
	}
}
