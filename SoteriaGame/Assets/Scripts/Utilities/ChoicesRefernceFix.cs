using UnityEngine;
using System.Collections;

public class ChoicesRefernceFix : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetDialogueFromChicue ( int i )
	{
		GameDirector.instance.GetDialogueFromChoice( i );

	}
}
