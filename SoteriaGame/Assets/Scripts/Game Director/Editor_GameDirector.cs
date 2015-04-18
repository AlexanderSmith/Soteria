// MyScriptEditor.cs
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(GameDirector))]
[CanEditMultipleObjects]
public class GameDirectorEditor : Editor {
	
	SerializedProperty valueProperty;
	
	void OnEnable() 
	{
		//valueProperty = this.serializedObject.FindProperty("_timer");
	}
	
	public override void OnInspectorGUI() 
	{
		this.serializedObject.Update();

		//EditorGUILayout.LabelField( new GUIContent("_timer").text );
		//EditorGUILayout.IntSlider(valueProperty, 1, 10, new GUIContent("My Value"));
		//this.serializedObject.ApplyModifiedProperties();


		//Add Tools to quickly Change the players Data!

		/*
		GUILayout.Button(" - ", GUILayout.Width(20f));


	
			
			if (GUILayout.Button(" - ", GUILayout.Width(20f)))
				EditorGUILayout.IntSlider(valueProperty, 1, 10, new GUIContent("Speed"));
		*/
	}
	
}