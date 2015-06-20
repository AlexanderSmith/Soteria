﻿using UnityEngine;
using System.Collections;

public class Qte_Handle : MonoBehaviour {
	
	public int QTECount;
	private Animator _QteAnim;
	
	// Use this for initialization
	void Awake () 
	{
		this._QteAnim = this.GetComponent<Animator>() as Animator;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.QTECount = GameDirector.instance.GetQTECount();
		_QteAnim.SetInteger("QTECount", QTECount);
	}
	
	public void Overcome()
	{
		_QteAnim.SetBool ("Overcome", true);
	}

	public void ResetOvercome()
	{
		_QteAnim.SetBool ("Overcome", false);
	}
	
	public void AddFear()
	{
		_QteAnim.SetBool("Encounter", true);
	}
	
	public void RemoveFear()
	{
		_QteAnim.SetBool("Encounter", false);
	}
}
