using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitiateDialogueSegment : MonoBehaviour {

    public Text SpeakerText;
    public Text ResponseOneText;
    public Text ResponseTwoText;
    public Text ResponseThreeText;

	private GameObject CanvResp1;
	private GameObject CanvResp2;
	private GameObject CanvResp3;


	public Text[]ResponsesText;

    private HUDManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("MCP").GetComponent<HUDManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay()
	{
		//BeginTextInteraction();
	}
	void OnTriggerEnter()
    {
        BeginTextInteraction();
    }
	void OnTriggerExit()
	{
		StopTextInteraction();
	}

	private void StopTextInteraction()
	{
		manager.EnableDialogue();
		CycleDiaglogueSegment(true);
		manager.DisableDialogue();
	}

    private void BeginTextInteraction()
    {
        manager.EnableDialogue();
        CycleDiaglogueSegment(true);
    }

    private void CycleDiaglogueSegment(bool value)
    {
		if (SpeakerText != null)
		{
			GameObject.FindGameObjectWithTag("SpeakerText").GetComponent<Text>().text = ResponseOneText.text;
			SpeakerText.gameObject.SetActive(value);
		}
		else
			GameObject.FindGameObjectWithTag("FirstResponse").SetActive(false);

		if (ResponseOneText != null)
		{
			if (CanvResp1 != null)
			{
				CanvResp1.GetComponent<Text>().text = ResponseOneText.text;
				CanvResp1.SetActive(value);
				
			}
			else
			{
				CanvResp1 = GameObject.FindGameObjectWithTag("FirstResponse");  

				GameObject.FindGameObjectWithTag("FirstResponse").GetComponent<Text>().text = ResponseOneText.text;
				GameObject.FindGameObjectWithTag("FirstResponse").SetActive(value);
			}
		}
		else
			GameObject.FindGameObjectWithTag("FirstResponse").SetActive(false);

		if (ResponseTwoText != null)
		{
			if (CanvResp2 != null)
			{
				CanvResp2.GetComponent<Text>().text = ResponseTwoText.text;
				CanvResp2.SetActive(value);
			}
			else
			{
				CanvResp2 = GameObject.FindGameObjectWithTag("SecondResponse"); 

				GameObject.FindGameObjectWithTag("SecondResponse").SetActive(value);
				GameObject.FindGameObjectWithTag("SecondResponse").GetComponent<Text>().text = ResponseTwoText.text;
			}
		}
		else
			GameObject.FindGameObjectWithTag("SecondResponse").SetActive(false);

		if (ResponseThreeText != null)
		{
			if (CanvResp3 != null)
			{
				CanvResp3.GetComponent<Text>().text = ResponseThreeText.text;
				CanvResp3.SetActive(value);
			}
			else
			{
				CanvResp3 = GameObject.FindGameObjectWithTag("ThirdResponse"); 
				GameObject.FindGameObjectWithTag("ThirdResponse").SetActive(value);
				GameObject.FindGameObjectWithTag("ThirdResponse").GetComponent<Text>().text = ResponseThreeText.text;
			}

		}
		else
			GameObject.FindGameObjectWithTag("ThirdResponse").SetActive(false);
    }
	public void GetResponseTexts(int Val)
	{
		SpeakerText.text = this.ResponsesText[Val].text;

		CanvResp1 = GameObject.FindGameObjectWithTag("FirstResponse");  
		CanvResp2 = GameObject.FindGameObjectWithTag("SecondResponse"); 
		CanvResp3 = GameObject.FindGameObjectWithTag("ThirdResponse"); 

		CanvResp1.gameObject.SetActive(false);
     	CanvResp2.gameObject.SetActive(false);
     	CanvResp3.gameObject.SetActive(false);

	}


}
