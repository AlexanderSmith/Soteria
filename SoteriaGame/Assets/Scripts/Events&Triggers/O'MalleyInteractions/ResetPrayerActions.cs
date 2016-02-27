using UnityEngine;
using System.Collections;

public class ResetPrayerActions : MonoBehaviour
{
	public void Failed()
	{
		this.transform.root.GetComponent<PrayerBools>().Failed();
		GameDirector.instance.EndTriggerState();
	}

	public void Success()
	{
		this.transform.root.GetComponent<PrayerBools>().Success();
		GameDirector.instance.EndTriggerState();
	}
}