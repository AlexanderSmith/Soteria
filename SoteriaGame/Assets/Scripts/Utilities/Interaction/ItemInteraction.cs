using UnityEngine;
using System.Collections;

public class ItemInteraction : InteractionBase
{	
	private GameObject _npcportrait;
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
	
	public override void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			}
			
			if (GameDirector.instance.GetPlayer().GetPlayerState() == PlayerState.Dialogue)
			{
				if (!GameDirector.instance.isDialogueActive())
				{
					this._reaction.execute();
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					GameDirector.instance.GetPlayer().PlayerActionPause();

					GameDirector.instance.SetupDialogue(DialogueName);
					GameDirector.instance.StartDialogue();
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
				}
			}
		}
	}
}