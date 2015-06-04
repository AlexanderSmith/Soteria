﻿using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

   // private GUIText instructionText;
    private ButtonController _coinController;
    private SceneFadeInOut _fade;
	private GameObject _HUDCanvas;

	// Use this for initialization
	void Awake () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	public void Update () {}

	public void Initialize()
	{
		/// Let's have another discussion later on if we want to use GUITExtures
		/// or UI elements. Not sure how easy it is to access the different stuff.
		/// we can try prototyping it later and on and see if it's worth it.

		//Initialize Instruction Text
     //   instructionText = (Instantiate(Resources.Load("Prefabs/GUIText")) as GameObject).guiText;
      //  instructionText.transform.position = new Vector3(0.01f, 0.95f, 0);
       // instructionText.text = "Test";
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

            //Initialize Coin
            //_coin = _HUDCanvas.transform.Find("Coin").gameObject;
            //_coin.GetComponent<ButtonController>().Initialize(this);

            //Initialize Fade out/in texture
            _fade = _HUDCanvas.GetComponentInChildren<SceneFadeInOut>();
        }
	}

    public void EnableEncounterView()
    {
        _coinController.Pulsing = true;
		FadeToBlack();
        //instructionText.enabled = false;
    }

    public void EnableNormalView()
    {
//        instructionText.enabled = true;
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
}
