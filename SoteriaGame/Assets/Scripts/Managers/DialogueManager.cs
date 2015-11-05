using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.UI;

enum DialogueState
{
	Active,
	Standby,
	Null
};
public class DialogueManager : MonoBehaviour 
{
	DialogueParser _parser;
	DialogueState _currState;
	DialogueData _diagdata;
	string 		UIText;
	
	/////////////////////////////////////////////////////////////////////////
	///////////////////////  INITIALIZATION   ///////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	public void Initialize()
	{
		this._currState = DialogueState.Standby;
		this._parser = new DialogueParser ();
	}

	public bool isDialogueActive()
	{
		return (_currState == DialogueState.Active ? true : false);
	}
	public void UpdateDialogueDataAudioID( AudioID inAid)
	{
		if (this._currState == DialogueState.Standby)
			this._diagdata.Aid = inAid;
	}
	
	public void readDialogueData(string txtname)
	{
		_parser.LoadDialogueSrc (_diagdata, txtname);
	}
	
	private void ChangeState(DialogueState inState)
	{
		_currState = inState;
	}

	public void ReloadDialogueData(string txtname, AudioID inAid)
	{
		if (this._currState == DialogueState.Standby) 
		{
			this._diagdata = new DialogueData(inAid);
			this._parser.LoadDialogueSrc (_diagdata,txtname);
		}
	}

	/////////////////////////////////////////////////////////////////////////
	///////////////////////  GUI MANIPULATION   /////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	private void ActivateGUI()
	{
	}
	
	private void DeActivateGUI()
	{
		
	}
	
	//////////////////////////////////////////////////////////////////////////	
	///////////////////////  DIALOGUE COMMAND CENTER /////////////////////////
	//////////////////////////////////////////////////////////////////////////
	
	
	public void StartDialogue()
	{
		this.ChangeState (DialogueState.Active);
		
		ActivateGUI ();
		
		GetNextLine();
		GetNextVO();
	}
	
	public void EndDialogue()
	{
		this.ChangeState (DialogueState.Standby);
		
		DeActivateGUI ();
	}
	
	void ContinueDialogue()
	{
		this._diagdata.Textindx++;
		
		GetNextLine();
		GetNextVO();
	} 
	
	/////////////////////////////////////////////////////////////////////////
	///////////////////////  TEXT MANIPULATION   ////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	void GetNextLine()
	{
		UIText = this._diagdata.DialogueLines[this._diagdata.Textindx];
	}
	
	public void SkipLine()
	{
		GetNextLine();
		GetNextVO();
	}
	
	/////////////////////////////////////////////////////////////////////////
	///////////////////////  AUDIO MANIPULATION   ///////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	void GetNextVO()
	{
		GameDirector.instance.PlayAudioClip(this._diagdata.Aid);	
	}
	
	void CheckNextClip()
	{
		if (this._currState == DialogueState.Active)
		{
			if ( !GameDirector.instance.isClipPlaying(this._diagdata.Aid))
			{
				if (this._diagdata.Textindx == this._diagdata.diaglength)
					EndDialogue();
				else
					ContinueDialogue();
			}			
		}
	}
	/////////////////////////////////////////////////////////////////////////
	//////////////////////  TRIGGER MANIPULATION   //////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	public void Update()
	{
		//TemporaryCode//
		//GameObject.Find("DiagText").GetComponent<Text>().text = UIText;	

		CheckNextClip();
	}
}
