using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueSkipController : MonoBehaviour 
{
	public Sprite regular_button_IMG;
	public Sprite highlighted_button_IMG;

	private Image imageScript;

	void Start()
	{
		imageScript = this.gameObject.GetComponent<Image> ();
		imageScript.overrideSprite = this.regular_button_IMG;
	}

	public void SkipDialogue()
	{
		GameDirector.instance.SkipLine ();
//		imageScript.overrideSprite = this.regular_button_IMG;
	}

//	public void HighlightButton()
//	{
//		imageScript.overrideSprite = this.highlighted_button_IMG;
//	}
//
//	public void RegularButton()
//	{
//		imageScript.overrideSprite = this.regular_button_IMG;
//	}
}
