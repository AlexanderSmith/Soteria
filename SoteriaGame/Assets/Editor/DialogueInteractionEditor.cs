using UnityEditor;
using UnityEngine;

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

