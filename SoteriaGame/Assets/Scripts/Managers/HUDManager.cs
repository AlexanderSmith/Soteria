using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

    private GUIText instructionText;
    private GUITexture coin;
    private GUITexture fadeTexture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Initialize()
	{
        instructionText = (Instantiate(Resources.Load("Prefabs/GUIText")) as GameObject).guiText;
        coin = (Instantiate(Resources.Load("Prefabs/GUITexture")) as GameObject).guiTexture;
        fadeTexture = (Instantiate(Resources.Load("Prefabs/GUITexture")) as GameObject).guiTexture;

        instructionText.transform.position = new Vector3(0.01f, 0.95f, 0);
        instructionText.text = "Test";
  
        coin.transform.position = new Vector3(0.09f, 0.16f, 0);
        coin.transform.localScale = new Vector3(0.175f, 0.35f, 0.1f);
        coin.texture = (Texture)Resources.Load("GUI/Soteria_coin");
        coin.gameObject.AddComponent<ButtonController>();

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
}
