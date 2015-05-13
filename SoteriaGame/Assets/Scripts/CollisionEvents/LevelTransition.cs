using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

	private LevelManager lvlManager;
	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		lvlManager = GameObject.Find ("MCP").GetComponent<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
		lvlManager.SetActiveLevel ("SafetyLight");
	}
}
