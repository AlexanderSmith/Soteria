using UnityEngine;
using System.Collections;

public class InteractionBase : MonoBehaviour {

	protected  GameObject _interactionbutton;
	
	// Use this for initialization
	public virtual void Awake () {
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}
	
	public virtual void TriggerEnter(Collider other)
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
	}
	
	public virtual void TriggerExit(Collider other)
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	public virtual void TriggerStay(Collider other)
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
	}
	
}
