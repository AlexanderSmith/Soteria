using UnityEngine;
using System.Collections;

public class DialogueInteraction : InteractionBase {
	private GameObject _npcportrait;

	public string DialogueName;
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
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);

		//if (other.gameObject.Equals(GameDirector.instance.GetPlayer()))
		//{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				GameDirector.instance.SetupDialogue(DialogueName, AudioID.None);
				GameDirector.instance.StartDialogue();
			}
		//}
	}
}
