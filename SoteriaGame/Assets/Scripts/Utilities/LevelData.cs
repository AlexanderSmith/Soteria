using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour
{
	// Use this for initialization
	void Awake () 
	{
		//Make sure everything loaded in
		if (GameObject.Find("MCP") == null)
			Instantiate(Resources.Load("Prefabs/MCP"), Vector3.zero, Quaternion.identity);
		
		if (GameObject.Find("UI") == null)
			Instantiate(Resources.Load("Prefabs/UI"), Vector3.zero, Quaternion.identity);
	}
}