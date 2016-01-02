using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DialogueData
{
	public DialogueData (AudioID inAid)
	{
		DialogueLines = new List<string>();
		Choices = new List<string>();
		TriggerCommands = new List<DialogueTrigger>();
		textindx = 0;
		diaglength = 0;
		currTrig = 0;
		this.Aid = inAid;
		hasChoices = false;
		hasTriggers = false;
	}
	
	private AudioID aid;
	public AudioID Aid
	{
		get{ return this.aid; }
		set{ aid = value;}
	}
	int textindx;
	public int Textindx
	{
		get { return this.textindx; }
		set { this.textindx = value; }
	}
	public bool hasChoices;
	public bool hasTriggers;
	private int currTrig;
	public int diaglength;
	public List<string> DialogueLines;
	public List<string> Choices;
	public List<DialogueTrigger> TriggerCommands;
}

public class DialogueTrigger
{
	string objectName;
	string MethodName;
	public int line;
	GameObject obj;
	
	public DialogueTrigger (string inOName, string inMName, int inLine)
	{
		this.line = inLine;
		this.objectName = inOName;
		this.obj = GameObject.Find(this.objectName);
		this.MethodName = inMName;
	}
	
	public string ObjectName
	{
		get { return this.objectName; }
		
		set { 
			this.objectName = value;
			this.obj = GameObject.Find(this.objectName);
		}
	}
	
	public void runTrigger ()
	{
		obj.SendMessage( this.MethodName );
	}
}

public class DialogueParser
{
	private string[] fileLines;
	
	public void LoadDialogueSrc (DialogueData inDialogue, string FileName) 
	{
		ParseDialogueFile(inDialogue, FileName);
		
		///Less Clips on audio/// FIX
		inDialogue.diaglength = inDialogue.DialogueLines.Count - 1;
	}
	
	// Use this for initialization
	void ParseDialogueFile(DialogueData inDialogue, string FileName) 
	{
		int linesadded = 0;
		// This is the folder name that holds all of the dialogue text files for each interaction
		TextAsset TextSource = Resources.Load("DialoguesSrc/" + FileName +"/Text/TextSrc") as TextAsset;
		
		fileLines = TextSource.text.Split ('\n');
		
		foreach(string line in fileLines)
		{
			if(line.Contains("<trigger/>"))
			{
				inDialogue.hasTriggers = true;
				//inDialogue.DialogueLines.Add("<trigger/>");
				string[] tricom = line.Split('-');
				tricom[0] = tricom[0].Replace("<trigger/>", "");
				DialogueTrigger tempTrig = new DialogueTrigger(tricom[0], tricom[1], linesadded);
				inDialogue.TriggerCommands.Add(tempTrig);
			}
			else if(line.Contains("<choice/>"))
			{
				inDialogue.hasChoices = true;
				string templine = line.Replace("<choice/>", "");
				inDialogue.Choices.Add(templine);
			}
			else
			{
				inDialogue.DialogueLines.Add(line);
				linesadded++;
			}
		}
	}
}