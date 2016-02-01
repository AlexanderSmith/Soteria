using UnityEngine;
using System.Collections;

public class OMalleyTutorialReactions : Reaction
{
	private bool _failed;
	private bool _succeeded;
	private bool _done;

	void Start()
	{
	}

	public override void execute()
	{
		this.GetBools();
		if (_done)
		{
			GameDirector.instance.ClearAudioList();
			GameDirector.instance.CheckLantern();
			GameDirector.instance.SuitRemoved();
			GameDirector.instance.TutorialCompleted();
			Application.LoadLevel("HubPass3");
		}
		else if (_failed)
		{
			this.transform.root.GetComponentInChildren<TriggerActions>().OMalleyOnSCOff();
			GameDirector.instance.GetDialogueFromReaction("OMalleyTeachingAnaToOvercomeFear3", this.gameObject.transform.parent.gameObject);
		}
		else if (_succeeded)
		{
			_done = true;
			this.transform.root.GetComponentInChildren<TriggerActions>().OMalleyOnSCOff();
			GameDirector.instance.GetDialogueFromReaction("AnaOMalleyAfterLingerSuccess", this.gameObject.transform.parent.gameObject);
		}
		else
		{
			GameDirector.instance.GetDialogueFromReaction("OMalleyTeachingAnaToOvercomeFear2", this.gameObject.transform.parent.gameObject);
		}
	}

	void GetBools()
	{
		this._failed = this.transform.root.GetComponent<InitiateTutorial>().GetFail();
		this._succeeded = this.transform.root.GetComponent<InitiateTutorial>().GetSuccess();
	}
}