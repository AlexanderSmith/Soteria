using UnityEngine;
using System.Collections;

public class TutorialDialogueInteraction : InteractionBase
{
	public string DialogueName;
	public Reaction _reaction;

	[HideInInspector]
	public bool EndsWithChoice = false;

	public override void Awake () 
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	public override void Update () 
	{
	}

	public override void TriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		}
	}
	
	public override void TriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{			
			if (GameDirector.instance.GetPlayer().GetPlayerState() == PlayerState.Dialogue && GameDirector.instance.GetDiagState() != DialogueState.TriggerActive)
			{
				if (!GameDirector.instance.isDialogueActive())
				{
					this._reaction.execute();
//					this.gameObject.transform.parent.GetComponent<SphereCollider>().isTrigger = false;
//					this.gameObject.transform.parent.GetComponent<SphereCollider>().enabled = false;
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
//					GameDirector.instance.GetPlayer().PlayerActionNormal();
				}
			}
//			else
//			{
//				if (Input.GetKeyDown(KeyCode.Space))
//				{
//					GameDirector.instance.GetPlayer().PlayerActionPause();
//					GameDirector.instance.SetupDialogue(DialogueName, this.transform.parent.gameObject);
//					GameDirector.instance.StartDialogue();
//					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
//				}
//			}
		}
	}

	public void ShowPrompt()
	{
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
	}
}