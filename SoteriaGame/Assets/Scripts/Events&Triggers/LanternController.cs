using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanternController : MonoBehaviour
{
	private bool _charged;

	void Start()
	{
		this._charged = true;
	}

	public void LanternClicked()
	{
		// _charged bool not working as intended (semi-works while debugging, but not normally)
		if (GameDirector.instance.GetGameState() == GameStates.Normal)// && this._charged)
		{
			Debug.Log ("Lantern Used");
			GameDirector.instance.LanternUsed();
			//this._charged = false;
		}
	}

	public void RechargeLantern()
	{
		this._charged = true;
	}
}
