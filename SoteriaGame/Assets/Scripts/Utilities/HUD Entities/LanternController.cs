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
		if (this._charged)
		{
			this.UseLantern();
		}
	}
	
	private void UseLantern()
	{
		GameDirector.instance.UseLantern();
		this._charged = false;
	}
	
	public void RechargeLantern()
	{
		this._charged = true;
		StartCoroutine("VisibleRecharge");
	}

	IEnumerator VisibleRecharge()
	{
		// Random value to make sure recharge doesn't happen before scene starts - 3 seconds is too long, 1 may be too short
		yield return new WaitForSeconds(1.0f);
		GameDirector.instance.PulseLantern();
		yield return new WaitForSeconds(2.0f);
		GameDirector.instance.IdleLantern();
	}

	public bool IsCharged()
	{
		return this._charged;
	}
}
