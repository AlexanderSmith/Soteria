using UnityEngine;
using System.Collections;

public class HidePlayer : InteractionBase
{
	Sprite hide;
	Sprite unhide;

	public override void Awake()
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		hide = Resources.Load<Sprite> ("GUI/SpaceButtonHide");
		unhide = Resources.Load ("GUI/SpaceButtonUnhide", typeof(Sprite)) as Sprite;
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
			this._interactionbutton.GetComponent<SpriteRenderer>().sprite = hide;
		}
	}
	
	public override void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			if (Input.GetKeyDown(KeyCode.Space))
			{
				GameDirector.instance.GetPlayer().PlayerActionHiding();
				this._interactionbutton.GetComponent<SpriteRenderer>().sprite = unhide;
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			}
		}
	}
}