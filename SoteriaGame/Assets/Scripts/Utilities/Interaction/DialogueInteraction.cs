using UnityEngine;
using System.Collections;

public class DialogueInteraction : InteractionBase {
	private GameObject _npcportrait;

	// Use this for initialization
	public override void Awake () 
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._npcportrait = this.transform.parent.FindChild("NPCPortrait").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	// Update is called once per frame
	public override void Update () 
	{
	
	}
	
	public override void TriggerStay(Collider other)
	{
		Debug.Log("Dialogue");
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			/// Temporrary code
			GameDirector.instance.StartDialogue(this.gameObject,other.transform.parent.gameObject);
			//GameDirector Temp = GameObject.Find("MCP").GetComponent<GameDirector>();
			//Temp.StartDialogue(this.gameObject, other.transform.parent.gameObject);
		}
	}
}
