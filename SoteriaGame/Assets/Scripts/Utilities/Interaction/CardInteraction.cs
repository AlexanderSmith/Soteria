using UnityEngine;
using System.Collections;

public class CardInteraction : InteractionBase
{	
	// Use this for initialization
	public override void Awake()
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	// Update is called once per frame
	public override void Update ()
	{	
	}
	
	public override void TriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
		}
	}
	
	public override void TriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		}
	}
	
	public override void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// Switch to splash screen
				GameDirector.instance.StartCardInteraction();
				GameDirector.instance.GetPlayer().PlayerActionCardPickup();
			}
		}
	}	
}