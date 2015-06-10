using UnityEngine;
using System.Collections;

public class InitiateDialogueSegment : MonoBehaviour {

    public GameObject SpeakerText;
    public GameObject ResponseOneText;
    public GameObject ResponseTwoText;
    public GameObject ResponseThreeText;

    public HUDManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("HUD").GetComponent<HUDManager>();
        CycleDiaglogueSegment(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            BeginTextInteraction();
        }
    }

    private void BeginTextInteraction()
    {
        manager.EnableDialogue();
        CycleDiaglogueSegment(true);
    }

    private void CycleDiaglogueSegment(bool value)
    {
        SpeakerText.SetActive(value);
        ResponseOneText.SetActive(value);
        ResponseTwoText.SetActive(value);
        ResponseThreeText.SetActive(value);
    }
}
