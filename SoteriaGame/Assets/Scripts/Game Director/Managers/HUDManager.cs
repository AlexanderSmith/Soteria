using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

    private GUIText instructionText;
    private GUITexture coin;
    private GUITexture fadeTexture;

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
        instructionText = (Instantiate(Resources.Load("Prefabs/GUIText")) as GameObject).guiText;
        instructionText.transform.position = new Vector3(0.01f, 0.95f, 0);
        instructionText.text = "Test";
  
		//Initialize Coin
		coin = (Instantiate(Resources.Load("Prefabs/GUITexture")) as GameObject).guiTexture;
        coin.transform.position = new Vector3(0.09f, 0.16f, 0);
        coin.transform.localScale = new Vector3(0.175f, 0.35f, 0.1f);
        coin.texture = (Texture)Resources.Load("GUI/Soteria_coin");
        coin.gameObject.AddComponent<ButtonController>();
        coin.gameObject.GetComponent<ButtonController>().Initialize(this);

		//Initialize Fade out/in texture
		fadeTexture = (Instantiate(Resources.Load("Prefabs/GUITexture")) as GameObject).guiTexture;
        fadeTexture.texture = (Texture)Resources.Load("GUI/FadeTexture");
        fadeTexture.gameObject.AddComponent<SceneFadeInOut>();
	}

    public void EnableEncounterView()
    {
        fadeTexture.GetComponent<SceneFadeInOut>().toGoToClear = false;
        coin.enabled = true;
        coin.GetComponent<ButtonController>().Pulsing = true;
        instructionText.enabled = false;
    }

    public void EnableNormalView()
    {
        instructionText.enabled = true;
        coin.enabled = true;
        coin.GetComponent<ButtonController>().Pulsing = false;
        fadeTexture.GetComponent<SceneFadeInOut>().toGoToClear = true;
    }

    public void SafteyLightButtonHit()
    {
        gameObject.GetComponent<GameDirector>().TakeSafteyLight(); //This feels weird here but may be the only place to call it from, we'll see later.
    }
}
