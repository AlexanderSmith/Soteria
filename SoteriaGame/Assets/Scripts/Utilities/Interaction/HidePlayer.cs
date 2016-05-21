using UnityEngine;
using System.Collections;

public class HidePlayer : InteractionBase
{
	Sprite hide;
	Sprite unhide;
	bool hiding;

	public override void Awake()
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
		hide = Resources.Load<Sprite> ("GUI/SpaceButtonHide");
		unhide = Resources.Load ("GUI/SpaceButtonUnhide", typeof(Sprite)) as Sprite;
		hiding = false;
	}

	public override void TriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			hiding = false;
		}
	}

	public override void TriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
			this._interactionbutton.GetComponent<SpriteRenderer>().sprite = hide;
			hiding = false;
		}
	}
	
	public override void TriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			if (Input.GetKeyDown(KeyCode.Space) && !hiding && GameDirector.instance.GetGameState() != GameStates.Hidden)
			{
 				hiding = true;
				GameDirector.instance.GetPlayer().PlayerActionHiding();
				GameDirector.instance.ChangeGameState(GameStates.Hidden);
				this._interactionbutton.GetComponent<SpriteRenderer>().sprite = unhide;
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			}
			else if (hiding && Input.GetKeyDown(KeyCode.Space) && GameDirector.instance.GetPlayer().GetComponent<Animator>().GetBool("HideIdle"))
			{
				this.hiding = false;
				this._interactionbutton.GetComponent<SpriteRenderer>().sprite = hide;
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			}
//			else if (Input.GetKeyDown(KeyCode.Space) && this._interactionbutton.GetComponent<SpriteRenderer>().sprite == unhide)
//			{
//				this.hiding = false;
//				this._interactionbutton.GetComponent<SpriteRenderer>().sprite = hide;
//				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
//			}
		}
	}
}