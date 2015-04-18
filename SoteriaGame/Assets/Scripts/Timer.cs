using UnityEngine;
using System.Collections;

//Basic Timer class, it can be used for pretty much anything
//Inputs will later use "coroutines" so for now we'll keep this
//but we should swap it out later.
//we can also have multiple timers for different things going on (Event timer, Input timer, puzzle timer etc..)
//To Avoid too many Components maybe turn this into a timer manager? with a list of timers?
public class Timer: MonoBehaviour
{
	private float elapsedTime; //Elapsed time from last Input;
	private bool started;
	
	// Use this for initialization
	void Start ()
	{
		this.enabled = false;
		started = false;
	}

	public void StartTimer()
	{
		started = true;
	}

	public void StopTimer()
	{
		ResetTimer();
		started = false;
	}

	public float ElapsedTime()
	{
		return elapsedTime;
	}

	public void ResetTimer()
	{
		elapsedTime = 0;
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (started)
			elapsedTime += Time.deltaTime;

		Debug.Log(elapsedTime);
	}
}
