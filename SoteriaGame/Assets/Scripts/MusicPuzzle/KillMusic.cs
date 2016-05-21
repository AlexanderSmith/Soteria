using UnityEngine;
using System.Collections;

public class KillMusic : MonoBehaviour
{
	void OnTriggerStay (Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				GameDirector.instance.StartFadeOutTimer ();
				Debug.Log ("Starting Fade Out Timer");
			}
		}
	}

	public void Update()
	{
		GameDirector.instance.FadeOut (AudioID.OrganMusic);
		GameDirector.instance.FadeOut (AudioID.BrassMusic);
		GameDirector.instance.FadeOut (AudioID.WindMusic);
		GameDirector.instance.FadeOut (AudioID.StringMusic);
	}
}
