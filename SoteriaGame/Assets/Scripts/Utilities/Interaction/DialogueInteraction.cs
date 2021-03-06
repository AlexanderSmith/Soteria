﻿using UnityEngine;
using System.Collections;

public class DialogueInteraction : InteractionBase
{
	public Sprite NpcPortrait;
	public bool IsNpcDialogue = true;

	public string DialogueName;
	public Reaction _reaction;
	
	public string FirstChoice;
	public string SecondChoice;
	public string ThirdChoice;
	
	[HideInInspector]
	public bool EndsWithChoice = false;

	// Use this for initialization
	public override void Awake () 
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	// Update is called once per frame
	public override void Update () 
	{

	}
	
	public override void TriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
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
					GameDirector.instance.GetPlayer().PlayerActionPause();
					GameDirector.instance.SetupDialogue(DialogueName, this.transform.parent.gameObject);
					if (this.IsNpcDialogue)
						GameDirector.instance.SetupDialogueNPC(this.NpcPortrait);
					if (this.EndsWithChoice)
						GameDirector.instance.SetupDialogueChoices(this.FirstChoice, this.SecondChoice, this.ThirdChoice);
					GameDirector.instance.StartDialogue(CanSkipDialogue);
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
				}
			}
		}
	}
}