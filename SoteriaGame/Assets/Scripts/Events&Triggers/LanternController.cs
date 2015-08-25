using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanternController : MonoBehaviour
{
	private bool _charged = false;
	public int _uses = 1;

	void Start()
	{
		Debug.Log (this._charged);
		this._charged = true;
		Debug.Log (this._charged);
	}

	void Update()
	{
//		if (!_charged)
//			Debug.Log ("false");
	}

	public void LanternClicked()
	{
		//Debug.Log (this._charged);
		// _charged bool not working as intended (semi-works while debugging, but not normally)
		if (GameDirector.instance.GetGameState() == GameStates.Normal)// && this._charged)
		{
			this.UseLantern();
		}
	}

	private void UseLantern()
	{
		if (this._charged)
		{
			GameDirector.instance.LanternUsed();
			this._charged = false;
			//this._uses--;
		}
	}

	public void RechargeLantern()
	{
		this._charged = true;
		this._uses = 1;
	}
}
