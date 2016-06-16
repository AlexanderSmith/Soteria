using UnityEngine;
using System.Collections;

public class Idling : MonoBehaviour
{
	private Animator _anim;

	void Start ()
	{
		this._anim = this.GetComponent<Animator>();
		this._anim.SetBool("Dialogue", true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.LookAt(GameDirector.instance.GetPlayer().gameObject.transform.position);
	}
}
