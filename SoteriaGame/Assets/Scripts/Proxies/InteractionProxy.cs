using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
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
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
	}
	void OnTriggerExit(Collider other)
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}

	void OnTriggerStay(Collider other)
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);

		if (Input.GetKeyDown(KeyCode.Space))
			GameDirector.instance.StartDialogue(this.gameObject, other.transform.parent.gameObject);
	}
}
