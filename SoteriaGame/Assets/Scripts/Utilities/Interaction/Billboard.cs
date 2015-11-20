using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {
	
	
	// Use this for initialization
	public void Awake () 
	{
	}
	
	// Update is called once per frame
	public void Update () 
	{
		transform.rotation = Camera.main.transform.rotation;
	}
}
