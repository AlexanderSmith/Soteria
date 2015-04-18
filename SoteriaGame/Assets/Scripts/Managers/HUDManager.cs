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
	    //IF STATEMANAGER IS IN ENCOUNTER MODE
        //PULSE COIN AND FADE
	}

	public void Initialize()
	{
        instructionText = Instantiate(Resources.Load("GUIText.prefab")) as GUIText;
        coin = Instantiate(Resources.Load("GUITexture.prefab")) as GUITexture;

        instructionText.transform.position = new Vector3(0.1f, 0.1f, 0);
        instructionText.text = "Test";
        
        coin.transform.position = new Vector3(0.1f, 0.9f, 0);
        coin.texture = (Texture)Resources.Load("GUI/Soteria_coin.png");
        
	}

    public void EnableEncounterView()
    {
        fadeTexture.enabled = true;
        coin.enabled = true;
        instructionText.enabled = false;
    }

    public void EnableNormalView()
    {
        instructionText.enabled = true;
        coin.enabled = true;
        fadeTexture.enabled = false;
    }
}
