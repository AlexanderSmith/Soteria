using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("1")) {
			Debug.Log("1 Pressed");
			Application.LoadLevel(0);
		}
	}

	public void Initialize()
	{

	}

	public void _Update()
	{
		
	}
}
