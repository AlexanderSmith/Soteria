using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    public bool toGoToClear;
	private Color _color;
    
	void Awake()
	{
		_color = Color.clear;
		toGoToClear = true;
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
		this._color = Color.Lerp(_color, Color.clear, fadeSpeed * Time.deltaTime);
		this.gameObject.GetComponent<Image>().color = _color;
    }


    public void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
		this._color = Color.Lerp(_color, Color.black, fadeSpeed * Time.deltaTime);
		this.gameObject.GetComponent<Image>().color = _color;
    }
}
