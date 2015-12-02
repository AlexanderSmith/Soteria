using UnityEngine;
using System.Collections;

public class CardInteraction : InteractionBase
{	
	private GameObject _npcportrait;
	private int _district;
	public string DialogueName;
	public Reaction _reaction;

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
	
//	public override void TriggerEnter(Collider player)
//	{
//		if (player.gameObject.tag == "Player")
//		{
//			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
//		}
//	}
//	
//	public override void TriggerExit(Collider player)
//	{
//		if (player.gameObject.tag == "Player")
//		{
//			this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
//		}
//	}
	
	public override void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			
			if (GameDirector.instance.GetPlayer().GetPlayerState() == PlayerState.Dialogue)
			{
				if (!GameDirector.instance.isDialogueActive())
				{
					this._reaction.execute();
					this.gameObject.transform.parent.GetComponent<SphereCollider>().isTrigger = false;
					this.gameObject.transform.parent.GetComponent<SphereCollider>().enabled = false;
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
					GameDirector.instance.GetPlayer().PlayerActionNormal();
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					GameDirector.instance.GetPlayer().PlayerActionCardPickup();
					// Convert tag string to int for district check to inform Game Director which district card belongs to
					char dist = this.gameObject.tag[0];
					this._district = dist - 48;
					GameDirector.instance.StartCardInteraction(this.gameObject.GetComponent<SpriteRenderer>().sprite, this._district,
					                                           this.transform.parent.parent.gameObject);
					GameDirector.instance.SetupDialogue(DialogueName);
					GameDirector.instance.StartDialogue();
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
				}
			}

//			if (Input.GetKeyDown(KeyCode.Space))
//			{
//				// Convert tag string to int for district check to inform Game Director which district card belongs to
//				char dist = this.gameObject.tag[0];
//				this._district = dist - 48;
//				GameDirector.instance.StartCardInteraction(this.gameObject.GetComponent<SpriteRenderer>().sprite, this._district,
//				                                           this.transform.parent.parent.gameObject);
//				GameDirector.instance.GetPlayer().PlayerActionCardPickup();
//			}
		}
	}
}