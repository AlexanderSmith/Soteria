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

			if (this._diagdata.hasChoices)
			{


			}
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

	private void LoadChoice(int indx)
	{
		if (_diagdata.Choices[indx].Length > 0)
		{
			switch( indx )
			{
				case 0:
					DialogueUIFirstChoice.transform.GetChild(0).GetComponent<Text>().text = _diagdata.Choices[indx];
				break;
				
				case 1:
					DialogueUISecondChoice.transform.GetChild(0).GetComponent<Text>().text = _diagdata.Choices[indx];
				break;

				case 2:
					DialogueUIThirdChoice.transform.GetChild(0).GetComponent<Text>().text = _diagdata.Choices[indx];
				break;
			}
		}
	}

	public void ActivateChoices()
	{
		if (_diagdata.hasChoices)
		{
			for (int i = 0; i < _diagdata.Choices.Count; ++i )
			{
				LoadChoice ( i );
			}
		}
	}

	public void LoadChoicesDialogueName (string fChoice, string sChoice, string tChoice)
	{
		this.FirstChoiceSrc = fChoice;
		this.SecondChoiceSrc = sChoice;
		this.ThirdChoiceSrc = tChoice;
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
		GameDirector.instance.StopAudioClip(this._diagdata.Aid);
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
		Text t = DialogueUIText.GetComponent<Text>();
		t.text = this._diagdata.DialogueLines[this._diagdata.Textindx];
	}
	
	public void SkipLine()
	{
		if (this._currState == DialogueState.Active)
		{
			if (this._diagdata.Textindx == this._diagdata.diaglength)
			{
				EndDialogue();
			}
			else
			{
				ContinueDialogue();
			}
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
			if (this._diagdata.Aid != AudioID.None)
			{
				if (!GameDirector.instance.isClipPlaying(this._diagdata.Aid))
				{
					if (this._diagdata.Textindx == this._diagdata.diaglength)
						EndDialogue();
					else
						ContinueDialogue();
				}
			}
		}
	}
	/////////////////////////////////////////////////////////////////////////
	//////////////////////  TRIGGER MANIPULATION   //////////////////////////
	/////////////////////////////////////////////////////////////////////////
	
	public void Update()
	{
		CheckNextClip();
	}
}
