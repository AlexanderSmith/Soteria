using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
	{
		this.enabled = false;
	}

	public void Initialize()
	{

	}
	
	// This gets called once per frame, switching to _Update to only call it
	// in the GameDirector is a lazy solution, we'll see if there's anyother way to do this.
	public void Update ()
	{

	}
}
