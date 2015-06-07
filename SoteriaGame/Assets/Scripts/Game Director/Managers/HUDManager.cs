using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

    private ButtonController _coinController;
    private SceneFadeInOut _fade;
	private GameObject _HUDCanvas;
    private GameObject _TextBubble;
    private GameObject _Portrait1;
    private GameObject _Portrait2;

	// Use this for initialization
	void Awake () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	public void Update () {}

	public void Initialize()
	{
        GameObject HUDElementStarter;
        if (HUDElementStarter = GameObject.Find("StartingHUDElements"))
        {
            foreach(string item in HUDElementStarter.GetComponent<ListContainer>()._list)
            {
                Instantiate(Resources.Load("Prefabs/"+item));
            }
        }
        else
        {
            _HUDCanvas = (Instantiate(Resources.Load("Prefabs/HUD")) as GameObject);
            _coinController = _HUDCanvas.GetComponentInChildren<ButtonController>();
            _coinController.Initialize(this);

            _Portrait1 = _HUDCanvas.transform.Find("Portrait1").gameObject;
            _Portrait2 = _HUDCanvas.transform.Find("Portrait2").gameObject;
            _TextBubble = _HUDCanvas.transform.Find("TextBubble").gameObject;

            CycleDiaglogueGUI(false);
            //Initialize Coin
            //_coin = _HUDCanvas.transform.Find("Coin").gameObject;
            //_coin.GetComponent<ButtonController>().Initialize(this);

            //Initialize Fade out/in texture
            _fade = _HUDCanvas.GetComponentInChildren<SceneFadeInOut>();
        }
	}

    public void EnableDialogue()
    {
        CycleDiaglogueGUI(true);
    }

    public void DisableDialogue()
    {
        EnableNormalView();
        CycleDiaglogueGUI(false);
    }

    public void EnableEncounterView()
    {
        _coinController.Pulsing = true;
		FadeToBlack();
    }

    public void EnableNormalView()
    { 
        _coinController.Pulsing = false;
		ClearFromBlack();
    }

	public void EnableSafetyLightView()
	{
		//Debug.Log ("In light view");
		_coinController.Pulsing = false;
		ClearFromBlack();
	}

	public void FadeToBlack()
	{
		_fade.toGoToClear = false;
	}

	public void ClearFromBlack()
	{
		_fade.toGoToClear = true;
	}

    public void SafteyLightButtonHit()
    {
		if (GameDirector.instance.CanUseToken ())
		{
			GameDirector.instance.TakeSafteyLight (); //This feels weird here but may be the only place to call it from, we'll see later.
			EnableSafetyLightView ();
		}
    }

    private void CycleDiaglogueGUI(bool value)
    {
        _Portrait1.SetActive(value);
        _Portrait2.SetActive(value);
        _TextBubble.SetActive(value);
    }
}
