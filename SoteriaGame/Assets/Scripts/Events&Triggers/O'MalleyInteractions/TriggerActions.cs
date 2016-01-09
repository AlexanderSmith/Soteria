using UnityEngine;
using System.Collections;

public class TriggerActions : MonoBehaviour
{
	private GameObject _oMalley;
	private GameObject _SC;
	private bool _startFade = false;
	private Color NewColor;

	Timer fadeTimer;

	void Start()
	{
		NewColor = new Color(0.0f,0.0f,0.0f,50.0f);

		this._oMalley = this.transform.parent.FindChild("pCube42").gameObject;
		this._SC = this.transform.parent.FindChild("Enemy").gameObject;
		fadeTimer = TimerManager.instance.Attach(TimersType.Tutorial);
	}

	void Update()
	{
		if (_startFade == true)
			GameDirector.instance.FadebyAmount(NewColor, 0.5f * Time.deltaTime);
	}

	public void OMalleyOffSCOn()
	{
		this._oMalley.SetActive(false);
		this._SC.SetActive(true);

		//GameDirector.instance.EndTriggerState();
	}

	public void FadeScreen()
	{
		GameDirector.instance.SetupScreenFade();
		_startFade = true;	
	}
}