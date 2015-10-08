using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanternController : MonoBehaviour
{
	private bool _charged = false;
	public int _uses = 1;
	
	void Start()
	{
		//Debug.Log (this._charged);
		this._charged = true;
		//Debug.Log (this._charged);
		// Send reference to Game Director
		GameDirector.instance.InitializeLanternController(this);
	}
	
	public void LanternClicked()
	{
		//Debug.Log (this._charged);
		if (GameDirector.instance.GetGameState() == GameStates.Normal && this._charged)
		{
			this.UseLantern();
		}
	}
	
	private void UseLantern()
	{
		if (this._charged)
		{
			GameDirector.instance.UseLantern();
			this._charged = false;
		}
	}
	
	public void RechargeLantern()
	{
		this._charged = true;
	}
}
