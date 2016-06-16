using UnityEngine;
using System.Collections;

public class ResetPrayerActions : MonoBehaviour
{
	public void Failed()
	{
		//this.transform.root.GetComponent<PrayerBools>().Failed();
		GameDirector.instance.EndDialogue();
		GameDirector.instance.GetDialogueFromReaction("AnaSoteriaPrayerFail", GameObject.Find("OMalleySoteriaPrayer"));
		GameDirector.instance.SetupDialogueChoices("AnaSoteriaPrayer1HUBp4", "AnaSoteriaPrayer2HUBp4", "");
		//GameDirector.instance.EndTriggerState();
	}

	public void Success()
	{
		this.transform.root.GetComponent<PrayerBools>().Success();
		GameDirector.instance.EndTriggerState();
	}
}