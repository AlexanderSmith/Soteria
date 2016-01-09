using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObservatoryPuzzleController : MonoBehaviour
{
	public GameObject tileUp;
	public GameObject tileDown;
	public GameObject tileLeft;
	public GameObject tileRight;
	public GameObject tileCtr;

	// Use this for initialization
	void Start ()
	{
		tileUp.GetComponentInChildren<Light>().enabled = false;
		tileDown.GetComponentInChildren<Light>().enabled = false;
		tileLeft.GetComponentInChildren<Light>().enabled = false;
		tileRight.GetComponentInChildren<Light>().enabled = false;
		tileCtr.GetComponentInChildren<Light>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}