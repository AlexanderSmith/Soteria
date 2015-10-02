using UnityEngine;
using System.Collections;

public class TempFortuneInteraction : MonoBehaviour {

//	private GameObject HudToken;
//	private GameObject HudLantern;
//	// Use this for initialization
//	void Awake () 
//	{
//		HudToken = GameObject.Find("Coin");
//		if (HudToken != null)
//			HudToken.SetActive(false);
//		HudLantern = GameObject.Find("Lantern");
//		if (HudLantern)
//			HudLantern.SetActive(false);
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if (HudToken == null) 
//		{
//			GameObject temp = GameObject.Find ("HUDInterface");
//			if (temp != null) //Quick hack, there's a frame delay between finding and keeping UI alive Investigate!
//			{
//				HudToken = temp.transform.FindChild("Coin").gameObject;
//				HudToken.SetActive(false);
//			}
//		}
//	}
	
	void OnTriggerEnter(Collider other)
	{
		//HudToken.SetActive(true);
		GameDirector.instance.TokenTrue ();
	}
}
