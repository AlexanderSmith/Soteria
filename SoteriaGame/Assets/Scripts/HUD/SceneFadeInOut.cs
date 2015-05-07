using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    public bool toGoToClear;
    
	void Awake()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		this.guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	void Start()
    {
		this.guiTexture.color = Color.clear;
    }

    void Update()
    {
		if (this.toGoToClear)
			this.FadeToClear();
        else
			this.FadeToBlack();
    }


    public void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
		this. guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    public void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
		this.guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }
}
