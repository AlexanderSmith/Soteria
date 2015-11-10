using UnityEngine;
using System.Collections;

public class CardInteraction : InteractionBase
{
	protected GameObject _interactionbutton;
	
	// Use this for initialization
	public virtual void Awake()
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{	
	}
	
	public virtual void TriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
		}
	}
	
	public virtual void TriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		}
	}
	
	public virtual void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// Switch to splash screen
				GameDirector.instance.GetPlayer().PlayerActionCardPickup();
			}
		}
	}	
}