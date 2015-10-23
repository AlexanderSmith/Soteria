using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	
	private GameObject _dialogueinterface;
	private DialogueChoice _dproxy;
	private Image _npcportrait;
	private Image _playerportrait;
	
	private Text _npctext;
	private Text[] _playerchoices;
	public bool isEnded = false;
	private bool isOngoing = false;

	public bool isCurrentlyActive()
	{
		return isOngoing;
	}
	public GameObject getDialogueInterface()
	{
		return this._dialogueinterface;
	}
	public void Initialize()
	{
		_playerchoices = new Text[3];
		
		_dialogueinterface = GameObject.Find("DialogueInterface");
		_npcportrait = _dialogueinterface.transform.FindChild("PortraitNPC").gameObject.GetComponent<Image>();
		_playerportrait = _dialogueinterface.transform.FindChild("PortraitPlayer").gameObject.GetComponent<Image>();
		_npctext = _dialogueinterface.transform.FindChild("Dialogue").gameObject.GetComponent<Text>();
		
		_playerchoices[0] = _dialogueinterface.transform.FindChild("Response 1").transform.Find("Text").gameObject.GetComponent<Text>();
		_playerchoices[1] = _dialogueinterface.transform.FindChild("Response 2").transform.Find("Text").gameObject.GetComponent<Text>();
		_playerchoices[2] = _dialogueinterface.transform.FindChild("Response 3").transform.Find("Text").gameObject.GetComponent<Text>();
		
		_npctext.gameObject.SetActive(false);
		_playerchoices[0].gameObject.SetActive(false);
		_playerchoices[1].gameObject.SetActive(false);
		_playerchoices[2].gameObject.SetActive(false);

		this.enabled = false;
	}
	
	public void startdialogue(GameObject NPC, GameObject Player)
	{
		this.isOngoing = true;

		this._npcportrait.overrideSprite = NPC.transform.Find("NPCPortrait").GetComponent<Image>().sprite;
		this._playerportrait.overrideSprite = Player.transform.Find("PlayerPortrait").GetComponent<Image>().sprite;
		
		this._dialogueinterface.transform.Find("TextBackGround").GetComponent<Animator>().SetBool("Show",true);
		this._dialogueinterface.transform.Find("PortraitNPC").GetComponent<Animator>().SetBool("Show",true);
		this._dialogueinterface.transform.Find("PortraitPlayer").GetComponent<Animator>().SetBool("Show",true);
		
		this._dproxy = NPC.transform.Find("DialogueText").GetComponent<DialogueTextProxy>().DialoguePrefab;
		SetupText();
		
	}
	
	public void SelectResponse(int resp)
	{
		if (!isEnded)
		{
			this._dproxy = this._dproxy.DialoguePrefabs[resp-1];
			
			if (this._dproxy.isEnd)
			{
				isEnded = true;
				stopdialogue();
			}
			else
			{
				SetupText();
			}
		}
		else
			isEnded = false;
	}
	
	private void SetupText()
	{
		setTextonDialogue(true);
		this._npctext.text = this._dproxy.NPCText;
		
		this._playerchoices[0].transform.parent.gameObject.SetActive(false);
		this._playerchoices[1].transform.parent.gameObject.SetActive(false);
		this._playerchoices[2].transform.parent.gameObject.SetActive(false);
		
		for (int i = 0; i< _dproxy.size; ++i)
		{
			this._playerchoices[i].text = this._dproxy.ChoicesText[i];
			this._playerchoices[i].transform.parent.gameObject.SetActive(true);
		}
	}

	public void EndDialogue()
	{
		this.stopdialogue();
	}
	
	private void stopdialogue()
	{
		this._playerchoices[0].transform.parent.gameObject.SetActive(true);
		this._playerchoices[1].transform.parent.gameObject.SetActive(true);
		this._playerchoices[2].transform.parent.gameObject.SetActive(true);
		
		this._dialogueinterface.transform.Find("TextBackGround").GetComponent<Animator>().SetBool("Show",false);
		this._dialogueinterface.transform.Find("PortraitNPC").GetComponent<Animator>().SetBool("Show",false);
		this._dialogueinterface.transform.Find("PortraitPlayer").GetComponent<Animator>().SetBool("Show",false);
		
		setTextonDialogue(false);
		this.isOngoing = false;
	}
	
	public void setTextonDialogue(bool state)
	{
		_npctext.gameObject.SetActive(state);
		_playerchoices[0].gameObject.SetActive(state);
		_playerchoices[1].gameObject.SetActive(state);
		_playerchoices[2].gameObject.SetActive(state);
	}
	
	// Update is called once per frame
	public void Update () 
	{
	}
}
