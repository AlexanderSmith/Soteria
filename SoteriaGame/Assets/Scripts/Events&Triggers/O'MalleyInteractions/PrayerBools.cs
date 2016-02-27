using UnityEngine;
using System.Collections;

public class PrayerBools : MonoBehaviour
{
	private bool _failed;
	private bool _success;
	public GameObject sotStatueCrumble2;
	public GameObject sotStatueCrumbled;
	private Material[] crumble2Mats;
	private Material[] crumbledMats;
	private Color alpha;
	Timer statueSwitch;
	
	void Start()
	{
		crumble2Mats = sotStatueCrumble2.GetComponent<MeshRenderer>().materials;
		foreach (Material mat in crumble2Mats)
		{
			mat.color = Color.gray;
		}
		crumbledMats = sotStatueCrumbled.GetComponent<MeshRenderer>().materials;
		alpha = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		statueSwitch = TimerManager.instance.Attach(TimersType.StatueSwitchTimer);
	}
	
	void Update()
	{
		if (statueSwitch.IsStarted())
		{
			if (statueSwitch.ElapsedTime() <= 5.0f)
			{
				crumble2Mats[0].color = Vector4.Lerp(crumble2Mats[0].color, crumble2Mats[0].color - alpha, Time.deltaTime);
				crumbledMats[0].color = Vector4.Lerp(crumbledMats[0].color, Color.gray, Time.deltaTime);
				crumble2Mats[1].color = Vector4.Lerp(crumble2Mats[1].color, crumble2Mats[1].color - alpha, Time.deltaTime);
				crumbledMats[1].color = Vector4.Lerp(crumbledMats[1].color, Color.gray, Time.deltaTime);
				crumble2Mats[2].color = Vector4.Lerp(crumble2Mats[2].color, crumble2Mats[2].color - alpha, Time.deltaTime);
				crumbledMats[2].color = Vector4.Lerp(crumbledMats[2].color, Color.gray, Time.deltaTime);
				crumble2Mats[3].color = Vector4.Lerp(crumble2Mats[3].color, crumble2Mats[3].color - alpha, Time.deltaTime);
				crumbledMats[3].color = Vector4.Lerp(crumbledMats[3].color, Color.gray, Time.deltaTime);
				crumble2Mats[4].color = Vector4.Lerp(crumble2Mats[4].color, crumble2Mats[4].color - alpha, Time.deltaTime);
				crumbledMats[4].color = Vector4.Lerp(crumbledMats[4].color, Color.gray, Time.deltaTime);
			}
			else
			{
				statueSwitch.StopTimer();
			}
		}
	}

	public void StartTimer()
	{
		statueSwitch.StartTimer();
	}

	public void Failed()
	{
		this._failed = true;
	}

	public void ResetFailed()
	{
		this._failed = false;
	}

	public void Success()
	{
		this._success = true;
	}

	public bool GetFailed()
	{
		return this._failed;
	}

	public bool GetSuccess()
	{
		return this._success;
	}
}