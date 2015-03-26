using UnityEngine;
using System.Collections;

public class InstructionPrompt : MonoBehaviour {
	
	public float timeDelay = 10;
	
	// Use this for initialization
	void Start () 
	{
		float timeLeft = 30.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeDelay -= Time.deltaTime;
		if ( timeDelay < 0 )
		{
			GameObject X =  GameObject.FindGameObjectWithTag("InstructionPrompt");
			X.SetActive(false);
		}
	}
}
