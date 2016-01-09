using UnityEngine;
using System.Collections;

public class OMalleyTutorialReactions : Reaction
{
	private bool _failed;
	private bool _succeeded;

	void Start()
	{
	}

	public override void execute()
	{
		this.GetBools();
		if (_failed)
		{
			GameDirector.instance.GetDialogueFromReaction("OMalleyTeachingAnaToOvercomeFear2", this.gameObject.transform.parent.gameObject);
			GameDirector.instance.StartDialogue();
		}
		else if (_succeeded)
		{
			GameDirector.instance.GetDialogueFromReaction("AnaOMalleyAfterLingerSuccess", this.gameObject.transform.parent.gameObject);
			GameDirector.instance.StartDialogue();
		}
		else
		{
			GameDirector.instance.GetDialogueFromReaction("OMalleyTeachingAnaToOvercomeFear2", this.gameObject.transform.parent.gameObject);
			GameDirector.instance.StartDialogue();
		}
	}

	void GetBools()
	{
		this._failed = this.transform.root.GetComponent<InitiateTutorial>().GetFail();
		this._succeeded = this.transform.root.GetComponent<InitiateTutorial>().GetSuccess();
	}
}