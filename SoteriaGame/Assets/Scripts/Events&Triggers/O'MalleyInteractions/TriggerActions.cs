using UnityEngine;
using System.Collections;

public class TriggerActions : MonoBehaviour
{
	private GameObject _oMalley;
	private GameObject _SC;

	Timer fadeTimer;

	void Start()
	{
		this._oMalley = this.transform.parent.FindChild("pCube42").gameObject;
		this._SC = this.transform.parent.FindChild("Enemy").gameObject;
		fadeTimer = TimerManager.instance.Attach(TimersType.Tutorial);
	}

	void Update()
	{
		if (fadeTimer.IsStarted())
		{
			if (fadeTimer.ElapsedTime() < 5)
			{
				GameDirector.instance.OMalleyEncounter();
			}
			else
			{
				fadeTimer.StopTimer();
				GameDirector.instance.SetClearStatus(true);
			}
		}
	}

	public void OMalleyOffSCOn()
	{
		this._oMalley.SetActive(false);
		this._SC.SetActive(true);
		//GameDirector.instance.EndTriggerState();
	}

	public void FadeScreen()
	{
		fadeTimer.StartTimer();
		GameDirector.instance.SetClearStatus(false);
	}
}