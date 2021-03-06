﻿using UnityEngine;
using System.Collections;

public class Hub1Dialogues : MonoBehaviour
{
	public GameObject cartographer;
	public GameObject pt;
	public GameObject _hubToMusic;

	private bool _cart;
	private bool _pt;
	private bool _allDialogues;
	private bool _noCartDiaglogue;

	void Start()
	{
		this._cart = GameDirector.instance.GetCompass();
		this._pt = GameDirector.instance.GetLantern();
		cartographer.SetActive(false);
		pt.SetActive(false);
		
		this._allDialogues = !this._cart;
		this._noCartDiaglogue = this._cart && !this._pt;

		if (this._allDialogues)
		{
			cartographer.SetActive(true);
			pt.SetActive(false);
		}

		else if (this._noCartDiaglogue)
		{
			cartographer.SetActive(false);
			pt.SetActive(true);
		}
	}
}