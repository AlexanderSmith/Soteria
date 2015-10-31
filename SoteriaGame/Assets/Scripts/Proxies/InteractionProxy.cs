using UnityEngine;
using System.Collections;

public class InteractionProxy : MonoBehaviour {

	private GameObject _interactionbutton;
	private GameObject _npcportrait;

	// Use this for initialization
	void Awake () 
	{
		this._interactionbutton = this.transform.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
		}
	}
}
