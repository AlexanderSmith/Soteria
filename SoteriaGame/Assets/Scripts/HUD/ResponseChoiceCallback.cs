using UnityEngine;
using System.Collections;

public class ResponseChoiceCallback : MonoBehaviour {

    public HUDManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("HUD").GetComponent<HUDManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        manager.DisableDialogue();
    }
}
