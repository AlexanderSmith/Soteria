using UnityEngine;
using System.Collections;

public class SoteriaStatueController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{
		//if (Clickable.Click(this.gameObject))
		//{
			Escape ();
		//}
	}

	void Escape()
	{
		Destroy(this);
		Application.LoadLevel("Basic");
	}
}
