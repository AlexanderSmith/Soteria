using UnityEngine;
using System.Collections;

public class SafetyLightController : MonoBehaviour {

	GameObject safeArea;

	public enum State
	{
		Finding,
		Moving
	};
	State currentState;

	float lightSpeed = 1f;

	// Use this for initialization
	void Start () {
		currentState = State.Finding;	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == State.Finding) 
		{
			FindNextClosest ();	
		}

		if (currentState == State.Moving) 
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, safeArea.transform.position, Time.deltaTime * lightSpeed);
			if (Vector3.Distance(this.transform.position, safeArea.transform.position) <= 2f)
			{
				currentState = State.Finding;
			}
		}
	}

	void FindNextClosest()
	{
		safeArea = GameObject.FindWithTag ("SafeArea");
		currentState = State.Moving;
	}
}
