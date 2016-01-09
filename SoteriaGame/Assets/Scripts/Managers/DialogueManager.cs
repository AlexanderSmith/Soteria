using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum DialogueState
{
	Active,
	Standby,
	Choice,
	TriggerActive,
	Null
};
public class DialogueManager : MonoBehaviour
{
	GameObject _activeActor;

	DialogueParser _parser;
	DialogueState _currState;
	DialogueData _diagdata;
	GameObject DialogueUIBG;
	GameObject DialogueUIText;
	
	GameObject DialogueUIFirstChoice;
	GameObject DialogueUISecondChoice;
	GameObject DialogueUIThirdChoice;
	
	string  UIText;
	string FirstChoiceSrc;
	string SecondChoiceSrc;
	string ThirdChoiceSrc;
	
	/////////////////////////////////////////////////////////////////////////
	///////////////////////  INITIALIZATION   ///////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	public void Initialize()
	{
		this._currState = DialogueState.Standby;
		this._parser = new DialogueParser ();
		InitializeInterface();
	}
	
	private void InitializeInterface ()
	{
		GameObject DialogueUI = GameObject.FindWithTag("DialogueInterface");
		DialogueUIBG = DialogueUI.transform.FindChild("TextBackGround").gameObject;
		DialogueUIBG.SetActive(false);
		DialogueUIText = DialogueUI.transform.FindChild("Dialogue").gameObject;
		DialogueUIText.SetActive(false);
		
		GameObject Choices = DialogueUI.transform.FindChild("Choices").gameObject;
		DialogueUIFirstChoice = Choices.transform.FindChild("FirstChoice").gameObject;
		DialogueUIFirstChoice.SetActive(false);
		DialogueUISecondChoice = Choices.transform.FindChild("SecondChoice").gameObject;
		DialogueUISecondChoice.SetActive(false);
		DialogueUIThirdChoice = Choices.transform.FindChild("ThirdChoice").gameObject;
		DialogueUIThirdChoice.SetActive(false);
	}
	
	public void OnLevelWasLoaded()
	{
		InitializeInterface();
	}
	
	public bool isDialogueActive()
	{
		return (_currState != DialogueState.Standby ? true : false);
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
	
	public void ReloadDialogueData(string txtname, GameObject inActor)
	{
		if (this._currState == DialogueState.Standby)
		{
			AudioID aid = GameDirector.instance.getIDByName(txtname);
			this._diagdata = new DialogueData(aid);

			this._activeActor = inActor;
			this._parser.LoadDialogueSrc (_diagdata,txtname);
		}
	}
	
	/////////////////////////////////////////////////////////////////////////
	///////////////////////  GUI MANIPULATION   /////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	private void ActivateGUI()
	{
		DialogueUIBG.SetActive(true);
		DialogueUIText.SetActive(true);
	}
	
	private void DeActivateGUI()
	{
		DialogueUIBG.SetActive(false);
		DialogueUIText.SetActive(false);
		DialogueUIFirstChoice.SetActive(false);
		DialogueUISecondChoice.SetActive(false);
		DialogueUIThirdChoice.SetActive(false);
	}
	
	private void privLoadChoice(int indx)
	{
		if (_diagdata.Choices[indx].Length > 0)
		{
			switch( indx )
			{
			case 0:
				DialogueUIFirstChoice.transform.GetChild(0).GetComponent<Text>().text = _diagdata.Choices[indx];
				DialogueUIFirstChoice.SetActive(true);
				break;
				
			case 1:
				DialogueUISecondChoice.transform.GetChild(0).GetComponent<Text>().text = _diagdata.Choices[indx];
				DialogueUISecondChoice.SetActive(true);
				break;
				
			case 2:
				DialogueUIThirdChoice.transform.GetChild(0).GetComponent<Text>().text = _diagdata.Choices[indx];
				DialogueUIThirdChoice.SetActive(true);
				break;
			}
		}
	}
	
	public void LoadChoices()
	{
		if (_diagdata.hasChoices)
		{
			for (int i = 0; i < _diagdata.Choices.Count; ++i )
			{
				privLoadChoice ( i );
			}
		}
	}
	

	public void DeactivateChoiceUI()
	{
		DialogueUIFirstChoice.SetActive(false);
		DialogueUISecondChoice.SetActive(false);
		DialogueUIThirdChoice.SetActive(false);
	}
	
	public void LoadChoicesDialogueName (string fChoice, string sChoice, string tChoice)
	{
		this._diagdata.hasChoices = true;
		this.FirstChoiceSrc = fChoice;
		this.SecondChoiceSrc = sChoice;
		this.ThirdChoiceSrc = tChoice;
	}
	
	//////////////////////////////////////////////////////////////////////////
	///////////////////////  DIALOGUE COMMAND CENTER /////////////////////////
	//////////////////////////////////////////////////////////////////////////
	public void StartDialogue()
	{
		if (this._diagdata.diaglength > 0)
		{
			this.ChangeState (DialogueState.Active);
			
			ActivateGUI ();
			
			GetNextLine();
			GetNextVO();
		}
	}
	
	public void EndDialogue()
	{
		this.ChangeState (DialogueState.Standby);
		GameDirector.instance.StopAudioClip(this._diagdata.Aid);
		DeActivateGUI ();
	}

	public void ClearLine()
	{
		Text t = DialogueUIText.GetComponent<Text>();
		t.text = "";
	}
	public void EndTriggerState()
	{
		if (this._currState == DialogueState.TriggerActive)
			this._currState = DialogueState.Active;
	}


	public void SetActiveActor( GameObject inActor )
	{
		this._activeActor = inActor;
	}

	public void GetDialogueFromChoice(int choice)
	{
		this.EndDialogue();
		switch (choice)
		{
			case 1:
				this.ReloadDialogueData( this.FirstChoiceSrc, this._activeActor);				
				GameDirector.instance.AttachAudioSource(this._activeActor, this.FirstChoiceSrc);
				GameDirector.instance.CollectAudioClipsForDialogue(this.FirstChoiceSrc);
				break;

			case 2:
				this.ReloadDialogueData( this.SecondChoiceSrc, this._activeActor);				
				GameDirector.instance.AttachAudioSource( this._activeActor, this.SecondChoiceSrc);
				GameDirector.instance.CollectAudioClipsForDialogue(this.SecondChoiceSrc);
			break;

			case 3:
				this.ReloadDialogueData( this.ThirdChoiceSrc, this._activeActor);				
				GameDirector.instance.AttachAudioSource( this._activeActor, this.ThirdChoiceSrc);
				GameDirector.instance.CollectAudioClipsForDialogue(this.ThirdChoiceSrc);
			break;
		}

		this.StartDialogue();
		this._diagdata.hasChoices = false;
	}

	public void GetDialogueFromReaction(string inFolderName, GameObject inActor)
	{
		this._activeActor = inActor;
		this.ReloadDialogueData(inFolderName, this._activeActor);				
		GameDirector.instance.AttachAudioSource(this._activeActor, inFolderName);
		GameDirector.instance.CollectAudioClipsForDialogue(inFolderName);
		this.StartDialogue();
	}

	void ExecuteTrigger()
	{
		DialogueTrigger tri = null;

		if (_diagdata.hasTriggers)
		{
			foreach (DialogueTrigger Tri in _diagdata.TriggerCommands)
			{
				if (_diagdata.Textindx == (Tri.line - 1))
				{
					Tri.runTrigger();
					tri = Tri;
				}
			}
		}
	
		///Make sure this dialogue doesn't happen multiple times or else it breaks.
		if (tri != null)
		{
			_diagdata.TriggerCommands.Remove(tri);
		}

		if ( _diagdata.TriggerCommands.Count < 1)
		{
			_diagdata.hasTriggers = false;
		}
	}

	void ContinueDialogue()
	{
 		this.ExecuteTrigger();
		if (this._currState != DialogueState.TriggerActive)
		{
			this._diagdata.Textindx++;
			
			GetNextLine();
			GetNextVO();
		}
	}
	
	/////////////////////////////////////////////////////////////////////////
	///////////////////////  TEXT MANIPULATION   ////////////////////////////
	/////////////////////////////////////////////////////////////////////////
	void GetNextLine()
	{
		Text t = DialogueUIText.GetComponent<Text>();
		t.text = this._diagdata.DialogueLines[this._diagdata.Textindx];
	}
	
	public void SkipLine()
	{
		if (this._currState == DialogueState.Active)
		{
			if (this._diagdata.diaglength > 0 )
			{
				if (this._diagdata.Textindx == this._diagdata.diaglength)
				{
					if (this._diagdata.hasChoices)
					{
						if (this._currState != DialogueState.Choice)
						{
							this._currState = DialogueState.Choice;
							this.LoadChoices();
						}
					}
					else if (this._diagdata.hasTriggers)
					{
						if (this._currState != DialogueState.TriggerActive)
						{
							
							this._currState = DialogueState.TriggerActive;
							this.ExecuteTrigger();
						}
					}
					else
						EndDialogue();
				}
				else
					ContinueDialogue();
			}
			else
				EndDialogue();
		}
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
			if (this._diagdata.Aid != AudioID.None && this._diagdata.diaglength > 0)
			{
				if (!GameDirector.instance.isClipPlaying(this._diagdata.Aid))
				{
					if (this._diagdata.Textindx == this._diagdata.diaglength)
					{
						if (this._diagdata.hasChoices)
						{
							if (this._currState != DialogueState.Choice)
							{
								this._currState = DialogueState.Choice;
								this.LoadChoices();
							}
						}
						else if (this._diagdata.hasTriggers)
						{
							if (this._currState != DialogueState.TriggerActive)
							{
								this._currState = DialogueState.TriggerActive;
								this.ExecuteTrigger();
							}
						}
						else
							EndDialogue();
					}
					else
						ContinueDialogue();
				}
			}
			else
				EndDialogue();
		}
	}
	/////////////////////////////////////////////////////////////////////////
	//////////////////////  TRIGGER MANIPULATION   //////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	public void Update()
	{
		CheckNextClip();
	}

	public DialogueState GetDiagState()
	{
		return this._currState;
	}
}
