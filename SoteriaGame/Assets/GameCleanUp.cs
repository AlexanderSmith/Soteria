using UnityEngine;
using System.Collections;

public class GameCleanUp : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		GameObject MCP = GameObject.Find("MCP");
		GameObject UI = GameObject.Find("UI");

		DestroyImmediate(MCP);
		DestroyImmediate(UI);
	}
}
