using UnityEngine;
using System.Collections;

public class TutorialSetUp : MonoBehaviour
{
	private GameObject _oMalley;

	void Awake()
	{
		this._oMalley = GameObject.Find("O'MalleyTutorial");
		this._oMalley.transform.FindChild("Enemy").gameObject.SetActive(false);
	}
}
