using UnityEngine;
using System.Collections;

public class TriggerActions : MonoBehaviour
{
	private GameObject _oMalley;
	private GameObject _SC;
	private bool _startFade = false;
	private Color NewColor;

	Timer lingerTimer;
	Timer eventTimer;

	void Start()
	{
		NewColor = new Color(0.0f,0.0f,0.0f,50.0f);

		this._oMalley = this.transform.parent.FindChild("pCube42").gameObject;
		this._SC = this.transform.parent.FindChild("Enemy").gameObject;
		lingerTimer = TimerManager.instance.Attach(TimersType.Tutorial);
		eventTimer = TimerManager.instance.Attach(TimersType.Tutorial);
	}

	void Update()
	{
		if (_startFade == true)
			GameDirector.instance.FadebyAmount(NewColor, 0.5f * Time.deltaTime);
		if (lingerTimer.IsStarted())
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				lingerTimer.ResetTimer();
			}
			else
			{
				if (lingerTimer.ElapsedTime() >= 5f)
				{
					lingerTimer.StopTimer();
					eventTimer.StopTimer();
					this.transform.root.GetComponent<InitiateTutorial>().Failed();
					GameDirector.instance.EndTriggerState();
					GameDirector.instance.EndDialogue();
				}
			}
			if (eventTimer.ElapsedTime() >= 10)
			{
				lingerTimer.StopTimer();
				eventTimer.StopTimer();
				this.transform.root.GetComponent<InitiateTutorial>().Succeeded();
				GameDirector.instance.EndTriggerState();
				GameDirector.instance.EndDialogue();
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
		GameDirector.instance.SetupScreenFade();
		_startFade = true;
		//GameDirector.instance.EndTriggerState();
	}

	public void PauseFade()
	{
		GameDirector.instance.PauseScreenFade();
		_startFade = false;
		//GameDirector.instance.EndTriggerState();
	}

	public void ResumeFade()
	{
		GameDirector.instance.ResumeScreenFade();
		_startFade = true;
		//GameDirector.instance.EndTriggerState();
	}

	public void InitiateEventTimers()
	{
		lingerTimer.StartTimer();
		eventTimer.StartTimer();
		//GameDirector.instance.EndTriggerState();
	}

	public void LingerPrompt()
	{
		this.transform.parent.GetComponentInChildren<TutorialDialogueInteraction>().ShowPrompt();
	}

	public void Delay()
	{
	}
}