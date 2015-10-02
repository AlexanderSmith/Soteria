using UnityEngine;
using System.Collections;

public class GetToken: MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		//HudToken.SetActive(true);
		GameDirector.instance.TokenTrue ();
	}
}
