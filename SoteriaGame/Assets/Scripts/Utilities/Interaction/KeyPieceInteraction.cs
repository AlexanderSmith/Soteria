using UnityEngine;
using System.Collections;

public class KeyPieceInteraction : InteractionBase
{
	public Reaction _reaction;
	public Sprite rightKey;
	
	// Use this for initialization
	public override void Awake()
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}

	public override void Update () 
	{
	}

	public override void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
				this.transform.root.gameObject.SetActive(false);
				GameDirector.instance.StartKeyInteraction(this.rightKey);
			}
		}
	}
}