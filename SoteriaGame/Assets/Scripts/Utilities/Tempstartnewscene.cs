using UnityEngine;
using System.Collections;

public class Tempstartnewscene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		 if (Input.GetKeyDown(KeyCode.LeftShift))
            Application.LoadLevel("Scene2");
	}
}
