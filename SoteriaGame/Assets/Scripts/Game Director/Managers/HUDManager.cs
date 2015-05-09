using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

   // private GUIText instructionText;
    private GameObject _coin;
    private GameObject _fade;
	private GameObject _HUDCanvas;

	// Use this for initialization
	void Awake () {
		this.enabled = false;
		_HUDCanvas = GameObject.Find("HUD");

		if (_HUDCanvas == null)
			_HUDCanvas = Instantiate(Resources.Load("Prefabs/HUD")) as GameObject;
	}
	
	// Update is called once per frame
	public void Update () {}

	public void Initialize()
	{
		//Initialize Coin
		_coin = _HUDCanvas.transform.Find("Coin").gameObject;
		_coin.GetComponent<ButtonController>().Initialize(this);

		//Initialize Fade out/in texture
		_fade = _HUDCanvas.transform.Find("BlackFade").gameObject;


	}

    public void EnableEncounterView()
    {
		_coin.SetActive(true);
		_coin.GetComponent<ButtonController>().Pulsing = true;
		_fade.GetComponent<SceneFadeInOut>().toGoToClear = false;
        //instructionText.enabled = false;
    }

    public void EnableNormalView()
    {
//        instructionText.enabled = true;
		_coin.SetActive(true);
		_coin.GetComponent<ButtonController>().Pulsing = false;
		_fade.GetComponent<SceneFadeInOut>().toGoToClear = true;
    }

    public void SafteyLightButtonHit()
    {
        this.gameObject.GetComponent<GameDirector>().TakeSafteyLight(); //This feels weird here but may be the only place to call it from, we'll see later.
    }


}
