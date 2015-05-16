using UnityEngine;
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

		//_QteAnim.SetBool("Encounter", GameDirector.instance.GetBool());
	}
}
