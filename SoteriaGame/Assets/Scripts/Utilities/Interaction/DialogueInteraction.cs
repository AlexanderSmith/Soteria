using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DialogueInteraction))]
public class DialogueInteractionEditor : Editor
{
	private SerializedObject m_Object;
	private SerializedProperty m_Property;
	
	void OnEnable() {
		m_Object = new SerializedObject(target);
	}
	public override void OnInspectorGUI()
	{
		m_Object.Update();
		//Draws the Script Field Before any Other Attribue//
		m_Property = m_Object.FindProperty("m_Script");
		EditorGUILayout.PropertyField(m_Property, new GUIContent("Script"), true);
		m_Object.ApplyModifiedProperties();
		
		m_Property = m_Object.FindProperty("_reaction");
		EditorGUILayout.PropertyField(m_Property, new GUIContent("Reaction"), true);
		m_Object.ApplyModifiedProperties();
		
		m_Property = m_Object.FindProperty("DialogueName");
		EditorGUILayout.PropertyField(m_Property, new GUIContent("Dialogue Name"), true);
		m_Object.ApplyModifiedProperties();
		
		//Changes the value of the bool based on the toggle//
		DialogueInteraction myScript = target as DialogueInteraction;
		myScript.EndsWithChoice = GUILayout.Toggle(myScript.EndsWithChoice, "Ends With Choice");
		
		if(myScript.EndsWithChoice)
		{
			//base.OnInspectorGUI();
			//Removes the Duplicate Script Field and Attributes Draws the rest of the Public Attributes//
			DrawPropertiesExcluding(m_Object, "m_Script", "_reaction", "DialogueName");
			m_Object.ApplyModifiedProperties();
			
		}
	}
}

public class DialogueInteraction : InteractionBase
{
	private GameObject _npcportrait;
	
	public string DialogueName;
	public Reaction _reaction;
	
	public string FirstChoice;
	public string SecondChoice;
	public string ThirdChoice;
	
	[HideInInspector]
	public bool EndsWithChoice = false;

	// Use this for initialization
	public override void Awake () 
	{
		this._interactionbutton = this.transform.parent.FindChild("InteractionButton").gameObject;
		this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
	}
	
	// Update is called once per frame
	public override void Update () 
	{

	}
	
	public override void TriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
				this._interactionbutton.GetComponent<Animator>().SetBool("Show", true);
			
			if (GameDirector.instance.GetPlayer().GetPlayerState() == PlayerState.Dialogue)
			{
				if (!GameDirector.instance.isDialogueActive())
				{
					this._reaction.execute();
					this.gameObject.transform.parent.GetComponent<SphereCollider>().isTrigger = false;
					this.gameObject.transform.parent.GetComponent<SphereCollider>().enabled = false;
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
					GameDirector.instance.GetPlayer().PlayerActionNormal();
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					GameDirector.instance.GetPlayer().PlayerActionPause();
					GameDirector.instance.SetupDialogue(DialogueName);
					if (this.EndsWithChoice)
						GameDirector.instance.SetupDialogueChoices(this.FirstChoice, this.SecondChoice, this.ThirdChoice);
					GameDirector.instance.StartDialogue();
					this._interactionbutton.GetComponent<Animator>().SetBool("Show", false);
				}
			}
		}
	}
}
